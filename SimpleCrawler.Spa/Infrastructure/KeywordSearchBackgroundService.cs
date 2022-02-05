using System;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleCrawler.Domain;
using SimpleCrawler.Domain.QueryKeywordContext;

namespace SimpleCrawler.SinglePageApp.Infrastructure
{
    public class KeywordSearchBackgroundService : RabbitListener
    {
        private readonly ILogger<KeywordSearchBackgroundService> _logger;

        // Because the Process function is a delegate callback, if you inject other services directly, they are not in one scope.
        // To invoke other Service instances, you can only use IServiceProvider CreateScope to retrieve instance objects
        private readonly ApplicationAdapter _applicationAdapter;

        public KeywordSearchBackgroundService(IServiceProvider services, 
            IOptions<AppConfiguration> options,
            ILogger<KeywordSearchBackgroundService> logger) : base(options)
        {
            RouteKey = "done.task";
            QueueName = "lemonnovelapi.chapter";
            _logger = logger;

            using var scope = services.CreateScope();
            _applicationAdapter = scope.ServiceProvider.GetRequiredService<ApplicationAdapter>();

        }

        protected override bool Process(string message)
        {
            var queryKeywordDto = JsonConvert.DeserializeObject<QueryKeywordDto>(message);
            
            try
            {
                _applicationAdapter.QueryProcessStart(queryKeywordDto);
                
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