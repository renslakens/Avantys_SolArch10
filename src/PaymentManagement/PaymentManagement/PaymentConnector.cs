using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace PaymentManagement {
    public class PaymentConnector {
        private readonly ConnectionFactory factory = new() { Uri = new Uri("amqp://guest:guest@localhost:5672") };
        private const string exchangeName = "SolArchExchange";
        private const string routingKey = "sol-arch-routing-key";
        private const string queueName = "SolArchQueue";

        public void SendPayment<T>(T messageObj) {
            using var connection = factory.CreateConnection("Rabbit Payment Sender");
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);

            string message = JsonSerializer.Serialize(messageObj);
            byte[] body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchangeName, routingKey, null, body);
        }

        public void ReceivePayment<T>() {
            using var connection = factory.CreateConnection("Rabbit Payment Receiver");
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);
            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, eventArgs) => {
                string message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                T messageObj = JsonSerializer.Deserialize<T>(message);
                Console.WriteLine($"Received message: {JsonSerializer.Serialize(messageObj, new JsonSerializerOptions { WriteIndented = true })}");
                channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            channel.BasicConsume(queueName, false, consumer);
            // Keep the channel open to listen for messages
            while (true) { }
        }
    }
}
