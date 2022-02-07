using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SimpleCrawler.Core.Domain;
using SimpleCrawler.Domain.QueryKeywordContext.QueryResultDetailAggregation;
using SimpleCrawler.Domain.QueryKeywordContext.QueryResultSummaryAggregation;

namespace SimpleCrawler.Domain.QueryKeywordContext.QueryKeywordAggregation
{
    public class QueryKeyword: EntityItem<QueryKeywordDto, QueryKeywordDbObject>
    {
        [JsonProperty] public string Id { get; }
        [JsonProperty] public Guid UserId { get; }
        [JsonProperty] public string Keyword { get; }

        [JsonProperty]
        public string TypeOfSearchEngine { get; }
        
        [JsonProperty] public QueryPeriod QueryPeriod { get; }

        [JsonProperty] public RowStatus RowStatus { get; }

        [JsonProperty] public DateTime InsertDate { get; }
        
        [JsonProperty]
        public QueryResultSummary QueryResultSummary { get; set; }
        
        [JsonProperty]
        public List<QueryResultDetail> QueryResultDetail { get; set; }

        public QueryKeyword(string id, Guid userId, string keyword, string searchEngine,
            QueryPeriod queryPeriod, RowStatus rowStatus, DateTime? insertDate)
        {
            Id = id;
            UserId = userId;
            Keyword = keyword;
            QueryPeriod = queryPeriod;
            TypeOfSearchEngine = searchEngine;
            RowStatus = rowStatus;
            InsertDate = insertDate ?? DateTime.UtcNow;
        }

        public override QueryKeywordDbObject GetDbObject()
        {
            return new QueryKeywordDbObject(Id, UserId, Keyword, TypeOfSearchEngine, QueryPeriod, 
                QueryResultSummary?.GetDbObject(), QueryResultDetail?.Select(x=> x.GetDbObject()).ToList(),
                RowStatus, InsertDate);
        }

        public override QueryKeywordDto GetDtoObject()
        {
            return new QueryKeywordDto(Id, UserId, Keyword, TypeOfSearchEngine, QueryPeriod,   
                QueryResultSummary?.GetDtoObject(), QueryResultDetail?.Select(x=> x.GetDtoObject()).ToList(),
                RowStatus, InsertDate);
        }

        public void UpdateSearchSummary(List<Uri> mentionUrls)
        {
            // QueryResultDto = mentionUrls;
        }
    }
}