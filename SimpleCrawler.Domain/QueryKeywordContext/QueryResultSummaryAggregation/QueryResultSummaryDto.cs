using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SimpleCrawler.Domain.QueryKeywordContext.QueryResultSummaryAggregation
{
    public class QueryResultSummaryDto
    {
        [JsonProperty] public Guid Id { get; }
        [JsonProperty] public Guid UserId { get; }
        [JsonProperty] public string Keyword { get; }

        [JsonProperty]
        public Type SearchEngine { get; }
        
        [JsonProperty] public QueryPeriod QueryPeriod { get; }
        
        [JsonProperty] public RowStatus RowStatus { get; }

        [JsonProperty] public DateTime InsertDate { get; }
        
        public QueryResultSummaryDto(Guid id, Guid userId, string keyword, Type searchEngine,
            QueryPeriod queryPeriod,  RowStatus rowStatus, DateTime? insertDate)
        {
            Id = id;
            UserId = userId;
            Keyword = keyword;
            QueryPeriod = queryPeriod;
            SearchEngine = searchEngine;
            RowStatus = rowStatus;
            InsertDate = insertDate ?? DateTime.UtcNow;
        }
    }
}