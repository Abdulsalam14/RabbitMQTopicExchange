
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQTopicExchange.Services
{
    public class TopicConsumerService: ITopicConsumerService
    {

        private readonly IConnection _connection;

        public TopicConsumerService(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<string> ConsumeMessage(string []args,string message)
        {

            using var channel = _connection.CreateModel();

            channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);
            var queueName = channel.QueueDeclare().QueueName;

            if (args.Length < 1)
            {
                //Console.Error.WriteLine("Usage: {0} [binding_key...]",
                //                        Environment.GetCommandLineArgs()[0]);
                //Console.WriteLine(" Press [enter] to exit.");
                //Console.ReadLine();
                //Environment.ExitCode = 1;
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
                Console.WriteLine($" [x] Received '{routingKey}':'{message}'");
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
            return " ";
        }
    }
}
