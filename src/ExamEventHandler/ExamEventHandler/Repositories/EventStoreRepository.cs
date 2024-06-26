using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using ExamManagement.Events;

namespace ExamManagement.Repositories
{
    public class EventStoreRepository
    {
        private readonly IMongoCollection<ExamScheduled> _eventExamCollection;

        public EventStoreRepository()
        {
            var client = new MongoClient("mongodb://admin:solarch10@172.19.0.1");
            var database = client.GetDatabase("ExamEvent");

            _eventExamCollection = database.GetCollection<ExamScheduled>("ExamEvents");
        }

        public async Task<bool> HandleMessageAsync(string messageType, string message)
        {
            try
            {
                switch (messageType)
                {
                    case "examScheduled":
                        // Deserialize the JSON message into ExamScheduled object
                        var jsonObj = JsonSerializer.Deserialize<ExamScheduled>(message, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true // Allows case-insensitive matching
                        });
                        if (jsonObj == null)
                        {
                            Console.WriteLine("Deserialized JSON object is null.");
                            return false;
                        }

                        jsonObj.CommandType = messageType;

                        await HandleScheduledExamAsync(jsonObj);
                        return true;
                    case "examGraded":
                        var jsonObj2 = JsonSerializer.Deserialize<ExamGraded>(message, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true // Allows case-insensitive matching
                        });
                        if (jsonObj2 == null)
                        {
                            Console.WriteLine("Deserialized JSON object is null.");
                            return false;
                        }

                        await HandleGradedExamAsync(jsonObj2);
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private async Task HandleGradedExamAsync(ExamGraded gradedExam)
        {
            try
            {
                gradedExam.EventDate = DateTime.UtcNow;

                // Insert into MongoDB collection
                await _eventExamCollection.InsertOneAsync(gradedExam);

                Console.WriteLine($"Inserted ExamGraded event with Id: {gradedExam.Id}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
       

        public async Task<bool> HandleScheduledExamAsync(ExamScheduled examScheduled)
        {
            try
            {
                examScheduled.EventDate = DateTime.UtcNow;

                // Ensure ScheduledDate is in UTC
                examScheduled.ScheduledDate = examScheduled.ScheduledDate.ToUniversalTime();

                // Insert into MongoDB collection
                await _eventExamCollection.InsertOneAsync(examScheduled);

                Console.WriteLine($"Inserted ExamScheduled event with Id: {examScheduled.Id}");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

    }
}