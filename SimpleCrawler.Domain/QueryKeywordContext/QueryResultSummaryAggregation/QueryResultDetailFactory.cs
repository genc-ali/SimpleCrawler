namespace SimpleCrawler.Domain.QueryKeywordContext.QueryResultSummaryAggregation
{
    public static class QueryResultSummaryFactory
    {
        public static QueryResultSummary GetQueryResultSummaryFromDto(QueryResultSummaryDto dto)
        {
            return new QueryResultSummary(dto.Id, dto.UserId, dto.Keyword, dto.SearchEngine, dto.QueryPeriod, 
                dto.RowStatus, dto.InsertDate);
        }

        public static QueryResultSummary GetQueryResultSummaryFromDbObject(QueryResultSummaryDbObject dbObject)
        {
            return new QueryResultSummary(dbObject.Id, dbObject.UserId, dbObject.Keyword, dbObject.SearchEngine,
                dbObject.QueryPeriod, dbObject.RowStatus, dbObject.InsertDate);
        }
    }
}