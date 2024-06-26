using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ScheduleManagement.Models;

public class Schedule {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public Class Class { get; set; }
}