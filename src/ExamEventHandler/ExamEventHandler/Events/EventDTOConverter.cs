using MongoDB.Bson.Serialization;
using MongoDB.Bson;

public static class EventDTOConverter {
    public static DTO ToDTO<T>(T obj, string eventType) {
        var document = obj.ToBsonDocument();
        
        return new DTO { EventDate = DateTime.UtcNow, EventType = eventType, Data = document };
    }

    //public static T FromDTO<T>(DTO dto) {
    //    if (dto.EventType != typeof(T).Name) {
    //        throw new InvalidOperationException($"Cannot convert DTO of type {dto.EventType} to {typeof(T).Name}");
    //    }

    //    return BsonSerializer.Deserialize<T>(dto.Data);
    //}

    //public static object FromDTO(DTO dto) {
    //    var type = Type.GetType($"ExamManagement.Events.{dto.EventType}");
    //    if (type == null) {
    //        throw new InvalidOperationException($"Type {dto.EventType} not found in namespace ExamManagement.Events");
    //    }

    //    return BsonSerializer.Deserialize(dto.Data, type);
    //}
}