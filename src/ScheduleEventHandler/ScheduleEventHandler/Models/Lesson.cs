using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ScheduleManagement.Models;

public class Lesson {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonConverter(typeof(ObjectIdJsonConverter))]
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public Class Class { get; set; }
    public Teacher Teacher { get; set; }
    public Module Module { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime StartDateTime { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime EndDateTime { get; set; }
    public string Classroom { get; set; }

    public Lesson() {
        Id = ObjectId.GenerateNewId();
    }
}