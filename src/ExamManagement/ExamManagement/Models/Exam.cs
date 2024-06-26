using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamManagement.Models
{
    public class Exam
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime ScheduledDate { get; set; }

        public string StudentId { get; set; }

        public Module Module { get; set; }

        public Proctor proctor { get; set; }

        public double? Grade { get; set; }

    }
}