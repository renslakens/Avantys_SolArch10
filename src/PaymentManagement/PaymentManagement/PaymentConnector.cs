using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace PaymentManagement {
    public class PaymentConnector {
        private readonly ConnectionFactory factory = new() { Uri = new Uri("amqp://guest:guest@localhost:5672") };
        private const string exchangeName = "PaymentSolArchExchange";
        private const string routingKey = "payment-sol-arch-routing-key";
        private const string queueName = "PaymentQueue";

        public void PaymentSender<T>(T messageObj, string CexchangeName, string CroutingKey, string CqueueName ) {
            // create connection
            using var connection = factory.CreateConnection("Rabbit Payment Sender");
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(CexchangeName, ExchangeType.Direct);
            channel.QueueDeclare(CqueueName, false, false, false, null);
            channel.QueueBind(CqueueName, CexchangeName, CroutingKey, null);

            // Serialize the message object and send it to the exchange
            string message = JsonSerializer.Serialize(messageObj);
            byte[] body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(CexchangeName, CroutingKey, null, body);

            // Close the channel and connection
            channel.Close();
            connection.Close();
        }

        public void PaymentReceiver<T>() {
            // create connection
            using var connection = factory.CreateConnection("Rabbit Payment Receiver");
            using var channel = connection.CreateModel();

            // Declare the exchange, queue, and bind the queue to the exchange
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);
            channel.BasicQos(0, 1, false);

            // Create a consumer to listen for messages
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, eventArgs) => {
                string message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                // Deserialize the message and print it to the console
                T messageObj = JsonSerializer.Deserialize<T>(message);
                Console.WriteLine($"Received message: {JsonSerializer.Serialize(messageObj, new JsonSerializerOptions { WriteIndented = true })}");

                // Acknowledge the message
                channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            // Start listening for messages
            channel.BasicConsume(queueName, false, consumer);
            // Keep the channel open to listen for messages
            while (true) { }
        }
    }
}
