using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Messaging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamManagement.Events
{
    public class ResultsPublished : Event
    {
        [BsonIgnoreIfDefault]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MongoId { get; set; }
        public  string Id { get; set; }
        public  string StudentId { get; set; }
        public  double Grade { get; set; }
        public  DateTime PublishedDate { get; set; }

        public ResultsPublished(Guid messageId, string examId, string studentId, double grade, DateTime publishedDate) : base(messageId)
        {
            this.Id = examId;
            this.StudentId = studentId;
            this.Grade = grade;
            this.PublishedDate = publishedDate;
        }
    }
}