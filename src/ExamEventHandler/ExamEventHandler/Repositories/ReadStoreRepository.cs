using System.Text.Json;
using ExamManagement.Models;
using MongoDB.Driver;

namespace ExamManagement.Repositories;

public class ReadStoreRepository
{
    private readonly IMongoCollection<Exam> _eventExamCollection;
    private readonly IMongoCollection<Student> _eventStudentCollection;

    public ReadStoreRepository()
    {
        var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_ADDRESS"));
        var database = client.GetDatabase("Exam");

        _eventExamCollection = database.GetCollection<Exam>("Exams");
        _eventStudentCollection = database.GetCollection<Student>("Students");
    }

    public async Task<bool> HandleMessageAsync(string messageType, string message)
    {
        try
        {
            switch (messageType)
            {
                case "examScheduled":
                    // Deserialize the JSON message into ExamScheduled object
                    var jsonObj = JsonSerializer.Deserialize<Exam>(message, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true // Allows case-insensitive matching
                    });
                    if (jsonObj == null)
                    {
                        Console.WriteLine("Deserialized JSON object is null.");
                        return false;
                    }

                    await HandleScheduledExamAsync(jsonObj);
                    return true;
                case "newStudents":
                    // Deserialize the JSON message into ExamScheduled object
                    var jsonObj1 = JsonSerializer.Deserialize<List<Student>>(message, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true // Allows case-insensitive matching
                    });
                    if (jsonObj1 == null)
                    {
                        Console.WriteLine("Deserialized JSON object is null.");
                        return false;
                    }

                    foreach (var student in jsonObj1)
                    {
                        await HandleNewStudentAsync(student);
                    }
                    return true;
                case "examGraded":
                    var jsonObj2 = JsonSerializer.Deserialize<Exam>(message, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true // Allows case-insensitive matching
                    });
                    if (jsonObj2 == null)
                    {
                        Console.WriteLine("Deserialized JSON object is null.");
                        return false;
                    }

                    await HandleGradedExamAsync(jsonObj2);
                    return true;

                default:
                    return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }


    public async Task<bool> HandleScheduledExamAsync(Exam examScheduled)
    {
        try
        {

            // Ensure ScheduledDate is in UTC
            examScheduled.ScheduledDate = examScheduled.ScheduledDate.ToUniversalTime();

            // Insert into MongoDB collection and replace if already exists
            await _eventExamCollection.ReplaceOneAsync(x => x.Id == examScheduled.Id, examScheduled, new ReplaceOptions { IsUpsert = true });

            Console.WriteLine($"Inserted ExamScheduled with Id: {examScheduled.Id}");
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> HandleGradedExamAsync(Exam jsonObj2)
    {
        try
        {
            var examToUpdate = await _eventExamCollection.Find(x => x.Id == jsonObj2.Id).FirstOrDefaultAsync();
            if (examToUpdate == null)
            {
                Console.WriteLine($"Exam with Id: {jsonObj2.Id} not found.");
                return false;
            }
            examToUpdate.Grade = jsonObj2.Grade;
            Console.WriteLine($"Updating Exam with Id: {jsonObj2.Id} with Grade: {jsonObj2.Grade} which is {examToUpdate}" );
            await _eventExamCollection.ReplaceOneAsync(x => x.Id == jsonObj2.Id, examToUpdate, new ReplaceOptions { IsUpsert = true });
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> HandleNewStudentAsync(Student student)
    {
        try
        {
            await _eventStudentCollection.ReplaceOneAsync(x => x.Id == student.Id, student, new ReplaceOptions { IsUpsert = true });
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}