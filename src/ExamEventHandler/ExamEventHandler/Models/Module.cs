using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamManagement.Models
{
    public class Module
    {
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        public string name { get; set; }
    }
}