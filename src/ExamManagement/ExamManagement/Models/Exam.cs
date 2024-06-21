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

        public DateTime scheduledDate { get; set; }

        public Student student { get; set; }
        
        public Module Module { get; set; }


        public double? grade { get; set; }

        // public int Credits { get; set; }

        //TODO: change to class model


        // //TODO: change to proctor model
        // public Proctor Proctor { get; set; }

        //TODO: change to module model


        // public bool IsReviewed { get; set; }

        // public string Classroom { get; set; } 
    }
}