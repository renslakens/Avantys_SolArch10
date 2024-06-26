using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ExamManagement.Models
{
public class ObjectIdJsonConverter : JsonConverter<ObjectId>
{
    public override ObjectId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = reader.GetString();
        if (ObjectId.TryParse(stringValue, out ObjectId objectId))
        {
            return objectId;
        }

        throw new JsonException($"Cannot convert {stringValue} to ObjectId");
    }

    public override void Write(Utf8JsonWriter writer, ObjectId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
}