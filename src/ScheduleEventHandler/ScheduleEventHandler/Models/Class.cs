using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ScheduleManagement.Models;

public class Class {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonConverter(typeof(ObjectIdJsonConverter))]
    public ObjectId Id { get; set; }
    public string ScheduleCode { get; set; }
    public List<Student> Students { get; set; }

    
    public Class() {
        if (Id == ObjectId.Empty) {
            Id = ObjectId.GenerateNewId();
        }
    }
    
}