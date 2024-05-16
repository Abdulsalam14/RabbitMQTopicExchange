namespace RabbitMQTopicExchange.Services
{
    public interface ITopicConsumerService
    {
        public Task<string> ConsumeMessage(string[] args);
    }
}
