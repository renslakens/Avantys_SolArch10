using System.Text.Json.Serialization;
using ExamManagement.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ScheduleManagement.Models;

public class Schedule {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonConverter(typeof(ObjectIdJsonConverter))]
    public ObjectId Id { get; set; }
    public Class Class { get; set; }
    
    public List<Lesson> Lessons { get; set; }

    public Schedule() {
        Id = ObjectId.GenerateNewId();
    }
}