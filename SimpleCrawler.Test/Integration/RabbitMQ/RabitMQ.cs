using System;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SimpleCrawler.SinglePageApp;
using Xunit;

namespace SimpleCrawler.Test.Integration.RabbitMQ
{
    public class RabbitMqClient
    {

        private readonly IModel _channel;
        private readonly ILogger _logger;

        public RabbitMqClient(ILogger<RabbitMqClient> logger)
        {
            AppConfiguration appConfiguration = new AppConfiguration();
            
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = appConfiguration.RabbitHost,
                    UserName = appConfiguration.RabbitUserName,
                    Password = appConfiguration.RabbitPassword,
                    Port = appConfiguration.RabbitPort,
                };
                
                var connection = factory.CreateConnection();
                _channel = connection.CreateModel();
            }
            catch (Exception ex)
            {
                logger.LogError(-1, ex, "RabbitMQClient init fail");
            }
            _logger = logger;
        }

        [Theory]
        [InlineData("routingKey", "TEST")]
        public void PushMessage(string routingKey, object message)
        {
            _logger.LogInformation("PushMessage, routingKey:{RoutingKey}", routingKey);
            
            _channel.QueueDeclare(queue: "message",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
            string msgJson = JsonConvert.SerializeObject(message);
            
            var body = Encoding.UTF8.GetBytes(msgJson);
            
            _channel.BasicPublish(exchange: "message",
                routingKey: routingKey,
                basicProperties: null,
                body: body);
        }
    }
}