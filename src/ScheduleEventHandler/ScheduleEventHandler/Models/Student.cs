using System.Text.Json.Serialization;
using ExamManagement.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ScheduleManagement.Models;

public class Student {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public bool IsAccepted { get; set; }
    public List<Exam> Exams { get; set; }
}