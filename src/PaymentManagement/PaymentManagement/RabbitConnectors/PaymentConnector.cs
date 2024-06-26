using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace PaymentManagement.RabbitConnectors
{
    public class PaymentConnector
    {
        private readonly ConnectionFactory factory = new() { Uri = new Uri(Environment.GetEnvironmentVariable("RABBIT_ADDRESS")) };
        private const string exchangeName = "SolArchExchange";
        private const string routingKey = "sol-arch-routing-key";
        private const string queueName = "PaymentQueue";

        public void PaymentSender<T>(T messageObj)
        {
            // create connection
            using var connection = factory.CreateConnection("Rabbit Payment Sender");
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            // Serialize the message object and send it to the exchange
            string message = JsonSerializer.Serialize(messageObj);
            byte[] body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchangeName, routingKey, null, body);

            // Close the channel and connection
            channel.Close();
            connection.Close();
        }

        public void PaymentReceiver<T>()
        {
            while (true)
            {
                try
                {
                    // create connection
                    using var connection = factory.CreateConnection("Rabbit Payment Receiver");
                    using var channel = connection.CreateModel();

                    // Declare the exchange, queue, and bind the queue to the exchange
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);
                    channel.QueueDeclare(queueName, false, false, false, null);
                    channel.QueueBind(queueName, exchangeName, routingKey, null);
                    channel.BasicQos(0, 1, false);

                    // Create a consumer to listen for messages
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, eventArgs) =>
                    {
                        string message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                        // Deserialize the message and print it to the console
                        T messageObj = JsonSerializer.Deserialize<T>(message);
                        Console.WriteLine($"Received message: {JsonSerializer.Serialize(messageObj, new JsonSerializerOptions { WriteIndented = true })}");

                        processMessage(message);

                        // Acknowledge the message
                        channel.BasicAck(eventArgs.DeliveryTag, false);
                    };

                    // Start listening for messages
                    channel.BasicConsume(queueName, false, consumer);

                    // Keep the channel open to listen for messages
                    while (true) { }

                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                    Console.WriteLine("Retrying in 5 seconds...");

                    // Wait for 5 seconds before retrying
                    Task.Delay(5000).Wait();
                }
            }
        }

        public void processMessage<T>(T message)
        {
        }

    }
}
