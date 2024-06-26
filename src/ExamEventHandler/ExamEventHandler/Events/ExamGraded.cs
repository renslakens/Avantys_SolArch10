using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Messaging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamManagement.Events
{
    public class ExamGraded : Event
    {
        [BsonIgnoreIfDefault]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MongoId { get; set; }
        public string Id { get; set; }
        public double Grade { get; set; }

        public ExamGraded(Guid messageId, string examId, double grade) : base(messageId)
        {
            this.Id = examId;
            this.Grade = grade;
        }
    }
}