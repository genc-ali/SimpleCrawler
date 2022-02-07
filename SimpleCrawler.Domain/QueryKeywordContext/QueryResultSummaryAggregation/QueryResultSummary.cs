using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SimpleCrawler.Core.Domain;
using SimpleCrawler.Domain.QueryKeywordContext.QueryKeywordAggregation;
using SimpleCrawler.Domain.QueryKeywordContext.QueryResultDetailAggregation;

namespace SimpleCrawler.Domain.QueryKeywordContext.QueryResultSummaryAggregation
{
    public class QueryResultSummary:EntityItem<QueryResultSummaryDto, QueryResultSummaryDbObject>
    {
        [JsonProperty] public Guid Id { get; }
        [JsonProperty] public Guid UserId { get; }
        [JsonProperty] public string Keyword { get; }

        [JsonProperty]
        public Type SearchEngine { get; }
        
        [JsonProperty] public QueryPeriod QueryPeriod { get; }
        
        [JsonProperty] public RowStatus RowStatus { get; }

        [JsonProperty] public DateTime InsertDate { get; }

        public QueryResultSummary(Guid id, Guid userId, string keyword, Type searchEngine,
            QueryPeriod queryPeriod, RowStatus rowStatus, DateTime? insertDate)
        {
            Id = id;
            UserId = userId;
            Keyword = keyword;
            QueryPeriod = queryPeriod;
            SearchEngine = searchEngine;
            RowStatus = rowStatus;
            InsertDate = insertDate ?? DateTime.UtcNow;
        }
        
        public override QueryResultSummaryDbObject GetDbObject()
        {
            return new QueryResultSummaryDbObject(Id, UserId, Keyword, SearchEngine, QueryPeriod,  RowStatus, InsertDate);
        }

        public override QueryResultSummaryDto GetDtoObject()
        {
            return new QueryResultSummaryDto(Id, UserId, Keyword, SearchEngine, QueryPeriod,  RowStatus, InsertDate);
        }
    }
}