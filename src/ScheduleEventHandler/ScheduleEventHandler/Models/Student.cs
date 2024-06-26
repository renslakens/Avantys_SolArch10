using System.Text.Json.Serialization;
using ExamManagement.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ScheduleManagement.Models;

public class Student {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string CompanyName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNuber { get; set; }
    public string Address { get; set; }
    public List<Exam> Exams { get; set; }
}