using ExamManagement.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ExamManagement.Events
{
    [BsonIgnoreExtraElements]
    public class ExamScheduled : Event
    {
        [BsonIgnoreIfDefault]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MongoId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        public string StudentId { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime ScheduledDate { get; set; }

        public Module Module { get; set; }

        public Proctor Proctor { get; set; }


        public ExamScheduled() : base(ObjectId.GenerateNewId())
        {
        }

        public ExamScheduled(ObjectId messageId, string id, string studentId, DateTime scheduledDate, Module module) : base(messageId)
        {
            this.Id = id;
            this.StudentId = studentId;
            this.ScheduledDate = scheduledDate;
            this.Module = module;
        }

    }

    public class Module
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }

    public abstract class Event
    {
        [BsonRepresentation(BsonType.String)]
        public ObjectId MessageId { get; private set; }

        protected Event(ObjectId messageId)
        {
            MessageId = messageId;
        }
    }
}