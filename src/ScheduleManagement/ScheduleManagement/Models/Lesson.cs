using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ScheduleManagement.Models;

public class Lesson {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public Class Class { get; set; }
    public Teacher Teacher { get; set; }
    public Module Module { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string Classroom { get; set; }
}