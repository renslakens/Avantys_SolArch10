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
            Console.WriteLine("Message Type: " + messageType);
            Console.WriteLine("Message: " + messageObj.ToString());
            string message = JsonSerializer.Serialize(messageObj);
            Console.WriteLine($"Sending message: {JsonSerializer.Serialize(messageObj, new JsonSerializerOptions { WriteIndented = true })}");
            byte[] body = Encoding.UTF8.GetBytes(message);

            IBasicProperties properties = channel.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object> { { "MessageType", messageType } };

            channel.BasicPublish(_exchangeName, _routingKey, properties, body);

            channel.Close();
            connection.Close();
        }
    }
}