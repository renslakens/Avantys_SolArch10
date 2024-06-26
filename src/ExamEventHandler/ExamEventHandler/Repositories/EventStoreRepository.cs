using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using ExamManagement.Events;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace ExamManagement.Repositories
{
    public class EventStoreRepository
    {
        private readonly IMongoCollection<DTO> _eventExamCollection;

        public EventStoreRepository()
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_ADDRESS"));
            var database = client.GetDatabase("ExamEvent");

            _eventExamCollection = database.GetCollection<DTO>("ExamEvents");
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


        public async Task<bool> HandleScheduledExamAsync(ExamScheduled examScheduled)
        {
            try
            {

                // Ensure ScheduledDate is in UTC
                examScheduled.ScheduledDate = examScheduled.ScheduledDate.ToUniversalTime();

                DTO dto = EventDTOConverter.ToDTO(examScheduled, "examScheduled");

                // Insert into MongoDB collection
                await _eventExamCollection.InsertOneAsync(dto);

                Console.WriteLine($"Inserted ExamScheduled event with Id: {examScheduled.Id}");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> HandleConductedExamAsync(ExamConducted examConducted) {
            try {

                // Ensure ConductedDate is in UTC
                examConducted.ConductedDate = examConducted.ConductedDate.ToUniversalTime();

                DTO dto = EventDTOConverter.ToDTO(examConducted, "examConducted");

                // Insert into MongoDB collection
                await _eventExamCollection.InsertOneAsync(dto);

                Console.WriteLine($"Inserted ExamConducted event with Id: {examConducted.Id}");
                return true;
            } catch (Exception e) {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> HandleGradedExamAsync(ExamGraded examGraded) {
            try {
                DTO dto = EventDTOConverter.ToDTO(examGraded, "examGraded");

                await _eventExamCollection.InsertOneAsync(dto);

                Console.WriteLine($"Inserted ExamGraded event with Id: {examGraded.Id}");
                return true;


            } catch (Exception e) {

                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> HandlePublishedExamAsync(ResultsPublished resultsPublished) {
            try {
                DTO dto = EventDTOConverter.ToDTO(resultsPublished, "resultsPublished");

                await _eventExamCollection.InsertOneAsync(dto);

                Console.WriteLine($"Inserted resultsPublished event with Id: {resultsPublished.Id}");
                return true;
            } catch (Exception e) {
                Console.WriteLine(e);
                return false;
            }
        }

    }
}
