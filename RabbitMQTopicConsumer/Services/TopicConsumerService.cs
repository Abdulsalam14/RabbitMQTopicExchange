
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQTopicExchange.Hubs;
using System.Text;

namespace RabbitMQTopicExchange.Services
{
    public class TopicConsumerService: ITopicConsumerService
    {

        private readonly IConnection _connection;
        private readonly IHubContext<MessageHub> _hubContext;

        public TopicConsumerService(IConnection connection, IHubContext<MessageHub> hubContext = null)
        {
            _connection = connection;
            _hubContext=hubContext;
        }

        public async Task<string> ConsumeMessage(string []args)
        {

            using var channel = _connection.CreateModel();

            channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);
            var queueName = channel.QueueDeclare().QueueName;

            if (args.Length < 1)
            {
                return "";
            }

            foreach (var bindingKey in args)
            {
                channel.QueueBind(queue: queueName,
                                  exchange: "topic_logs",
                                  routingKey: bindingKey);
            }

            Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var routingKey = ea.RoutingKey;

                //Console.WriteLine($" [x] Received '{routingKey}':'{message}'");
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            //Console.ReadLine();
            return " ";
        }
    }
}
