﻿using MongoDB.Driver;
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
            var client = new MongoClient("mongodb://admin:solarch10@172.20.0.1");
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
                examScheduled.EventDate = DateTime.UtcNow;

                // Ensure ScheduledDate is in UTC
                examScheduled.ScheduledDate = examScheduled.ScheduledDate.ToUniversalTime();

                DTO dto = EventDTOConverter.ToDTO(examScheduled, "ExamScheduled");

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

    }
}
