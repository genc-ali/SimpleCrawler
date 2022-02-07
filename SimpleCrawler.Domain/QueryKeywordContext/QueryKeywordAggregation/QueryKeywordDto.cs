using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SimpleCrawler.Core.Security;
using SimpleCrawler.Domain.QueryKeywordContext.QueryResultDetailAggregation;
using SimpleCrawler.Domain.QueryKeywordContext.QueryResultSummaryAggregation;

namespace SimpleCrawler.Domain.QueryKeywordContext.QueryKeywordAggregation
{
    public class QueryKeywordDto
    {
        [JsonProperty] public string Id { get; }
        [JsonProperty] public Guid UserId { get; }
        [JsonProperty] public string Keyword { get; }

        [JsonProperty] public string TypeOfSearchEngine { get; }

        [JsonProperty] public QueryPeriod QueryPeriod { get; }

        [JsonProperty] public RowStatus RowStatus { get; }

        [JsonProperty] public DateTime InsertDate { get; }

        [JsonProperty] public QueryResultSummaryDto QueryResultSummary { get; }

        [JsonProperty]  public List<QueryResultDetailDto> QueryResultDetails { get; }

        [JsonConstructor]
        public QueryKeywordDto(string id, Guid userId, string keyword, string typeOfSearchEngine,
            QueryPeriod queryPeriod,  QueryResultSummaryDto queryResultSummary,
            List<QueryResultDetailDto> queryResultDetails, RowStatus rowStatus, DateTime? insertDate)
        {
            Id = id ?? (userId.ToString() + "|" + keyword).GetMd5Hash();
            UserId = userId;
            Keyword = keyword;
            QueryPeriod = queryPeriod;
            TypeOfSearchEngine = typeOfSearchEngine;
            RowStatus = rowStatus;
            QueryResultSummary = queryResultSummary;
            QueryResultDetails = queryResultDetails;
            InsertDate = insertDate ?? DateTime.UtcNow;
        }

        public QueryKeywordDto(Guid userId, string keyword, Type searchEngine,
            QueryPeriod queryPeriod) : this(null, userId, keyword, searchEngine.FullName,
            queryPeriod,  null, null, RowStatus.Wait ,null)
        {

        }
    }
}