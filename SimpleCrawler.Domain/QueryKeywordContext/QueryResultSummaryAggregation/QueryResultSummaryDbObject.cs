using System;

namespace SimpleCrawler.Domain.QueryKeywordContext.QueryResultSummaryAggregation
{
    public class QueryResultSummaryDbObject
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Keyword{ get; set; }
        public Type SearchEngine { get; set; }
        public QueryPeriod QueryPeriod { get; set; }
        public RowStatus RowStatus{ get; set; }
        public DateTime InsertDate { get; set; }
        
        public QueryResultSummaryDbObject(Guid id, Guid userId, string keyword, Type searchEngine,
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