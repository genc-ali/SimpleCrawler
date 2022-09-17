using System;
using RabbitMQ.Client;

namespace SimpleCrawler.Core.DateTime
{
    public static class DateTimeExtension
    {
        public static AmqpTimestamp ToAmqpTimestamp(this System.DateTime datetime)
        {
            var epoch = new System.DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var unixTime = (datetime.ToUniversalTime() - epoch).TotalSeconds;
            var timestamp = new AmqpTimestamp((long)unixTime);
            return timestamp;
        }
        
        public static System.DateTime ToDateTime(this AmqpTimestamp datetime)
        {
            System.DateTime dtDateTime = new System.DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds( datetime.UnixTime ).ToLocalTime();
            return dtDateTime;
        }
    }
    
}