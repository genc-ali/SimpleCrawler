using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleCrawler.Domain.QueryKeywordContext;
using SimpleCrawler.Domain.QueryKeywordContext.QueryKeywordAggregation;

namespace SimpleCrawler.Domain
{
    public interface IQueryKeywordRepository
    {
        Task<QueryKeywordDbObject> SaveSearchSummaryAsync(QueryKeywordDbObject queryKeyword);
        Task<List<QueryKeywordDbObject>> GetAllKeywords();
        Task<QueryKeywordDbObject> InsertNewQueryKeyword(QueryKeywordDbObject queryKeyword);
        Task<QueryKeywordDbObject> GetKeywordByUser(Guid userId, string queryKeyword);
    }
}