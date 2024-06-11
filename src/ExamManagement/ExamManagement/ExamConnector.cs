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
        private readonly ConnectionFactory factory = new() { Uri = new Uri("amqp://guest:guest@localhost:5672") };
        private const string _exchangeName = "ExamSolArchExchange";
        private const string _routingKey = "exam-sol-arch-routing-key";
        private const string _queueName = "ExamQueue";

        public void SendExam<T>(T messageObj, string exchangeName, string routingKey, string queueName)
        {
            var connection = factory.CreateConnection("Rabbit Exam Sender");
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            string message = JsonSerializer.Serialize(messageObj);
            byte[] body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchangeName, routingKey, null, body);

            channel.Close();
            connection.Close();
        }

        public void ReceiveExam<T>()
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

                T messageObj = JsonSerializer.Deserialize<T>(message);
                Console.WriteLine($"Resceived message: {JsonSerializer.Serialize(messageObj, new JsonSerializerOptions { WriteIndented = true })}");

                channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            channel.BasicConsume(_queueName, false, consumer);
            while (true) { }
        }
    }
}