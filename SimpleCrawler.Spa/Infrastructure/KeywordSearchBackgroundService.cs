using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace SimpleCrawler.SinglePageApp.Infrastructure
{
    public class KeywordSearchBackgroundService : RabbitListener
    {
        private readonly ILogger<KeywordSearchBackgroundService> _logger;

        // Because the Process function is a delegate callback, if you inject other services directly, they are not in one scope.
        // To invoke other Service instances, you can only use IServiceProvider CreateScope to retrieve instance objects
        private readonly IServiceProvider _services;

        public KeywordSearchBackgroundService(IServiceProvider services, 
            IOptions<AppConfiguration> options,
            ILogger<KeywordSearchBackgroundService> logger) : base(options)
        {
            RouteKey = "done.task";
            QueueName = "lemonnovelapi.chapter";
            _logger = logger;
            _services = services;

        }

        protected override bool Process(string message)
        {
            var taskMessage = JToken.Parse(message);
            
            try
            {
                using var scope = _services.CreateScope();
                var xxxService = scope.ServiceProvider.GetRequiredService<XXXXService>();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Process fail,error:{ExMessage},stackTrace:{ExStackTrace},message:{QueMessage}",
                    ex.Message, ex.StackTrace, message);
                
                _logger.LogError(-1, ex, "Process fail");
                return false;
            }

        }
    }
}