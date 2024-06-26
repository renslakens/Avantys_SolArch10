using System.Text.Json;
using ExamManagement.Events;
using ExamManagement.Models;
using MongoDB.Driver;

namespace ExamManagement.Repositories;

public class ReadStoreRepository {
    private readonly IMongoCollection<Exam> _eventExamCollection;

    public ReadStoreRepository()
    {
        var client = new MongoClient("mongodb://admin:solarch10@172.19.0.1");
        var database = client.GetDatabase("Exam");

        _eventExamCollection = database.GetCollection<Exam>("Exams");
    }
    
            public async Task<bool> HandleMessageAsync(string messageType, string message)
            {
                try
                {
                    switch (messageType)
                    {
                        case "examScheduled":
                            // Deserialize the JSON message into ExamScheduled object
                            var jsonObj = JsonSerializer.Deserialize<Exam>(message, new JsonSerializerOptions
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
            
            public async Task<bool> HandleScheduledExamAsync(Exam examScheduled)
            {
                try
                {
    
                    // Ensure ScheduledDate is in UTC
                    examScheduled.ScheduledDate = examScheduled.ScheduledDate.ToUniversalTime();
    
                    // Insert into MongoDB collection and replace if already exists
                    await _eventExamCollection.ReplaceOneAsync(x => x.Id == examScheduled.Id, examScheduled, new ReplaceOptions { IsUpsert = true });
    
                    Console.WriteLine($"Inserted ExamScheduled with Id: {examScheduled.Id}");
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
}