namespace SimpleCrawler.Domain.QueryKeywordContext
{
    public enum RowStatus
    {
        None = 0,
        Wait = 10,
        Processing = 11,
        Completed = 12,
        Failed = 50,
        Deleted = 51
    }
}