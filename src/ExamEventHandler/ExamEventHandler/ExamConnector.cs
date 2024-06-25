using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using ExamManagement.Repositories;

namespace ExamManagement
{
    public class ExamConnector
    {
        private readonly ConnectionFactory factory = new() { Uri = new Uri(Environment.GetEnvironmentVariable("RABBIT_ADDRESS")) };
        private const string _exchangeName = "SolArchExchange";
        private const string _routingKey = "sol-arch-routing-key";
        private const string _queueName = "ExamQueue";
        private EventStoreRepository _eventStoreRepository;
        private ReadStoreRepository _readStoreRepository;
        
        public ExamConnector(EventStoreRepository eventStoreRepository, ReadStoreRepository readStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
            _readStoreRepository = readStoreRepository;
            
        }

        public void Receive<T>()
        {
            var connection = factory.CreateConnection("Rabbit Exam Receiver");
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(_exchangeName, ExchangeType.Fanout);
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
                string messageType = "MessageType";
               
                if (eventArgs.BasicProperties.Headers != null) {
                    foreach (var header in eventArgs.BasicProperties.Headers) {
                        if (header.Key == "MessageType") {
                            messageType = Encoding.UTF8.GetString((byte[])header.Value);
                        }
                    }
                }
                Console.WriteLine("Messagetype" + messageType);

                _eventStoreRepository.HandleMessageAsync(messageType, message);
                _readStoreRepository.HandleMessageAsync(messageType, message);
                
                channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            channel.BasicConsume(_queueName, false, consumer);
            while (true) { }
        }
    }
}