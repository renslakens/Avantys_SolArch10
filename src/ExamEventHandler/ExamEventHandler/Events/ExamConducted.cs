using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Messaging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamManagement.Events
{
    [BsonIgnoreExtraElements]
    public class ExamConducted : Event
    {
        [BsonIgnoreIfDefault]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MongoId { get; set; }
        public string Id { get; set; }
        public DateTime ConductedDate { get; set; }

        public ExamConducted() : base(ObjectId.GenerateNewId()) { }
        public ExamConducted(ObjectId messageId, string examId, DateTime conductedDate) : base(messageId)
        {
            this.Id = examId;
            this.ConductedDate = conductedDate;
        }
    }
}