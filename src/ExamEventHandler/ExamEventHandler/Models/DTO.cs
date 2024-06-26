
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class DTO {
    public string EventType { get; set; }
    [BsonElement("data")]
    public BsonDocument Data { get; set; }
}

