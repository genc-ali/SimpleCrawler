using SimpleCrawler.Domain.QueryKeywordContext;

namespace SimpleCrawler.Domain
{
    public interface IApplicationAdapter
    {
        public void QueryProcessStart(QueryKeywordDto queryKeywordDto);
    }
}