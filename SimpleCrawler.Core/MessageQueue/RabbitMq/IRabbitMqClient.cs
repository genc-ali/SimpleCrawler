namespace SimpleCrawler.Core.MessageQueue.RabbitMq
{
    public interface IRabbitMqClient
    {
        public void SendMessage(byte[] body, string routingKey, System.DateTime addMinutes);
    }
}