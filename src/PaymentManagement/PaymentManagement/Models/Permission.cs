using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PaymentManagement.Models {
    public class Permission {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
