
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQTopicClient.Services
{
    public class EchangeTopicService : IExchangeTopicService
    {
        private readonly IConnection _connection;

        public EchangeTopicService(IConnection connection)
        {
            _connection = connection;
        }

        public Task SendMessage(string[] args,string message)
        {
            using var channel = _connection.CreateModel();

            channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);
            foreach (var routingKey in args)
            {
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "topic_logs",
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($" [x] Sent '{routingKey}':'{message}'");
            }
            return Task.CompletedTask;
        }

    }
}
