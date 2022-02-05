using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SimpleCrawler.SinglePageApp.Infrastructure
{
    public class RabbitListener : IHostedService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        protected RabbitListener(IOptions<AppConfiguration> options)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    // This is my side of the configuration, you can use it yourself.
                    HostName = options.Value.RabbitHost,
                    UserName = options.Value.RabbitUserName,
                    Password = options.Value.RabbitPassword,
                    Port = options.Value.RabbitPort,
                };
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RabbitListener init error,ex:{ex.Message}");
            }
        }

        protected string RouteKey;
        protected string QueueName;

        // Method of handling messages
        protected virtual bool Process(string message)
        {
            throw new NotImplementedException();
        }

        // Registered Consumer Monitor Here
        private void Register()
        {
            Console.WriteLine($"RabbitListener register,routeKey:{RouteKey}");
            _channel.ExchangeDeclare(exchange: "message", type: "topic");
            _channel.QueueDeclare(queue: QueueName, exclusive: false);
            _channel.QueueBind(queue: QueueName,
                exchange: "message",
                routingKey: RouteKey);
          
            var consumer = new EventingBasicConsumer(_channel);
            
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.Span;
                var message = Encoding.UTF8.GetString(body);
                var result = Process(message);
                if (result)
                {
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
            };
            
            _channel.BasicConsume(queue: QueueName, consumer: consumer);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this._connection.Close();
            return Task.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Register();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _connection.Close();
            _connection?.Dispose();
            _channel?.Dispose();
        }
    }
}