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
        public  string Id { get; set; }
        public  string StudentId { get; set; }
        public  DateTime ConductedDate { get; set; }

        public ExamConducted(Guid messageId, string examId, string studentId, DateTime conductedDate) : base(messageId)
        {
            this.Id = examId;
            this.StudentId = studentId;
            this.ConductedDate = conductedDate;
        }
    }
}