
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class DTO {
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime EventDate { get; set; }
    public string EventType { get; set; }
    [BsonElement("data")]
    public BsonDocument Data { get; set; }
}

