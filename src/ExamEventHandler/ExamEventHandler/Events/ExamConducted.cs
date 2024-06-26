using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Messaging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamManagement.Events
{
    public class ExamConducted : Event
    {
        [BsonIgnoreIfDefault]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MongoId { get; set; }
        public readonly string examId;
        public readonly string studentId;
        public readonly DateTime conductedDate;

        public ExamConducted(Guid messageId, string examId, string studentId, DateTime conductedDate) : base(messageId)
        {
            this.examId = examId;
            this.studentId = studentId;
            this.conductedDate = conductedDate;
        }
    }
}