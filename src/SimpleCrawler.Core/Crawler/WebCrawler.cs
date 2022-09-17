using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleCrawler.Core.Crawler
{
    public  abstract class WebCrawler
    {
        public abstract Task<List<Uri>> GetMentions(int pageSize, string keyword, int pageIndex = 0, string initialHashCode = "");
    }
}