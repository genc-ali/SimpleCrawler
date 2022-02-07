using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SimpleCrawler.Core.DateTime;
using SimpleCrawler.Core.MessageQueue.RabbitMq;
using SimpleCrawler.Domain;
using SimpleCrawler.Domain.QueryKeywordContext;

namespace SimpleCrawler.SinglePageApp.Infrastructure
{
    public class RabbitListener : IHostedService
    {
        private readonly AppConfiguration _appConfiguration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private const int MaxWaitTime = 5 * 60 * 1000;
        private const int MinWaitTime = 1000;
        private int _waitTime = MinWaitTime;
        
        protected RabbitListener(AppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
            try
            {
                var uri = new Uri(
                    $"amqp://{_appConfiguration.UserName}:{_appConfiguration.Password}@{_appConfiguration.Host}:{_appConfiguration.Port}/CUSTOM_HOST");
            
                var factory = new ConnectionFactory
                {
                    Uri = uri
                };
                
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RabbitListener init error,ex:{ex.Message}");
            }
        }

        // Method of handling messages
        protected virtual Task<QueryPeriod> Process(string message)
        {
            throw new NotImplementedException();
        }

        // Registered Consumer Monitor Here
        private void Register()
        {
            Console.WriteLine($"RabbitListener register,routeKey:{_appConfiguration.MessageRouteKey}");
            
            _channel.ExchangeDeclare(_appConfiguration.MessageExchange, ExchangeType.Direct, true, false, null);           
            _channel.QueueDeclare(_appConfiguration.MessageQueue, true, false, false, null);           
            _channel.QueueBind(_appConfiguration.MessageQueue, _appConfiguration.MessageExchange, "NEW");
          
            var consumer = new EventingBasicConsumer(_channel);
            
            consumer.Received += (model, ea) =>
            {
                DateTime messageDate = ea.BasicProperties.Timestamp.ToDateTime();
                
                if ( messageDate > DateTime.UtcNow)
                {
                    _channel.BasicNack(ea.DeliveryTag, false, true);
                    
                    Thread.Sleep(_waitTime);
                    _waitTime += _waitTime;
                    
                    return;
                }

                _waitTime = MinWaitTime;
                
                var body = ea.Body.Span;
                var message = Encoding.UTF8.GetString(body);
                var result = Process(message).Result;
                if (result != QueryPeriod.None)
                {
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                
                // TODO:requeue for next process. update message timestamp and send to queue
                
            };
            
            _channel.BasicConsume(queue: _appConfiguration.MessageQueue, consumer: consumer);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Close();
            _connection?.Dispose();
            _channel?.Dispose();
            GC.Collect();
            return Task.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Register();
            return Task.CompletedTask;
        }

    }
}