using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PaymentManagement.Models {
    public class Payment {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id = new ObjectId();
        public double Amount { get; set; }
        public string Description { get; set; }
        public Student Student { get; set; }
        public DateTime PaymentDate { get; set; }
        
    }
}
