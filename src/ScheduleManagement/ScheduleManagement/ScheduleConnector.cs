using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ScheduleManagement;

public class ScheduleConnector {
    private readonly ConnectionFactory factory = new() { Uri = new Uri(Environment.GetEnvironmentVariable("RABBIT_ADDRESS")) };
    private const string exchangeName = "SolArchExchange";
    private const string routingKey = "sol-arch-routing-key";
    private const string queueName = "ScheduleQueue";

    public void ScheduleSender<T>(string messageType, T messageObj) {
        // create connection
        using var connection = factory.CreateConnection("Rabbit Schedule Sender");
        using var channel = connection.CreateModel();
        
        channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);
        channel.QueueDeclare(queueName, false, false, false, null);
        channel.QueueBind(queueName, exchangeName, routingKey, null);

        // Serialize the message object and send it to the exchange
        string message = JsonSerializer.Serialize(messageObj);
        byte[] body = Encoding.UTF8.GetBytes(message);
        
        IBasicProperties properties = channel.CreateBasicProperties();
        properties.Headers = new Dictionary<string, object> { { "MessageType", messageType } };
        
        channel.BasicPublish(exchangeName, routingKey, properties, body);

        // Close the channel and connection
        channel.Close();
        connection.Close();
    }
    
}