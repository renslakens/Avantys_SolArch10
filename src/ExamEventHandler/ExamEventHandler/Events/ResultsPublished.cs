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
    public class ResultsPublished : Event
    {
        [BsonIgnoreIfDefault]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MongoId { get; set; }
        public string Id { get; set; }
        public DateTime PublishedDate { get; set; }

        public ResultsPublished() : base(ObjectId.GenerateNewId()) { }
        public ResultsPublished(ObjectId messageId, string examId, DateTime publishedDate) : base(messageId)
        {
            this.Id = examId;
            this.PublishedDate = DateTime.Now;
        }
    }
}