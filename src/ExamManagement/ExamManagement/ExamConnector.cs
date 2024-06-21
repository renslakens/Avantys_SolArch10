using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ExamManagement
{
    public class ExamConnector
    {
        private readonly ConnectionFactory factory = new() { Uri = new Uri(Environment.GetEnvironmentVariable("RABBIT_ADDRESS")) };
        private const string _exchangeName = "SolArchExchange";
        private const string _routingKey = "sol-arch-routing-key";
        private const string _queueName = "ExamQueue";

        public async Task Send<T>(string messageType, T messageObj)
        {
            var connection = factory.CreateConnection("Rabbit Exam Sender");
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(_exchangeName, ExchangeType.Fanout);
            channel.QueueDeclare(_queueName, false, false, false, null);
            channel.QueueBind(_queueName, _exchangeName, _routingKey, null);

            Console.WriteLine($"Sending message");
            string message = JsonSerializer.Serialize(messageObj);
            byte[] body = Encoding.UTF8.GetBytes(message);

            IBasicProperties properties = channel.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object> { { "MessageType", messageType } };

            channel.BasicPublish(_exchangeName, _routingKey, properties, body);

            channel.Close();
            connection.Close();
        }

        public void Receive<T>()
        {
            var connection = factory.CreateConnection("Rabbit Exam Receiver");
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(_queueName, false, false, false, null);
            channel.QueueBind(_queueName, _exchangeName, _routingKey, null);
            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, eventArgs) =>
            {
                string message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                Console.WriteLine($"Received message");
                T messageObj = JsonSerializer.Deserialize<T>(message);
                Console.WriteLine($"Received message: {JsonSerializer.Serialize(messageObj, new JsonSerializerOptions { WriteIndented = true })}");

                channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            channel.BasicConsume(_queueName, false, consumer);
            while (true) { }
        }
    }
}