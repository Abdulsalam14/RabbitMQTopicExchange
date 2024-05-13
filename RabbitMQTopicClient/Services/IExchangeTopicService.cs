namespace RabbitMQTopicClient.Services
{
    public interface IExchangeTopicService
    {
        public Task SendMessage(string[] args,string message);
    }
}
