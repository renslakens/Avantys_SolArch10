using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ScheduleManagement.Models;

public class Class {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    
    public string Id { get; set; }
    public string ScheduleCode { get; set; }
    public List<Student> Students { get; set; }
}