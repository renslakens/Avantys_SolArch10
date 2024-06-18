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

        public string Name { get; set; }

        // public DateTime StartDateTime { get; set; }

        // public DateTime EndDateTime { get; set; }

        // public double Grade { get; set; }

        // public int Credits { get; set; }

        // //TODO: change to class model
        // public Class Class { get; set; }

        // //TODO: change to proctor model
        // public Proctor Proctor { get; set; }

        // //TODO: change to module model
        // public Module Module { get; set; }

        // public bool IsReviewed { get; set; }

        // public string Classroom { get; set; } 
    }
}