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
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime ScheduledDate { get; set; }

        public string StudentId { get; set; }
        
        public Module module { get; set; }

        public Proctor proctor { get; set;}

        public double? grade { get; set; }

    }
}


