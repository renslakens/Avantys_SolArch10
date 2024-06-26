using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ScheduleManagement.Models;

namespace ScheduleManagement.Services;

public class LessonService {
    private readonly IMongoCollection<Lesson> _lessonsCollection;
    
    public LessonService(IOptions<ScheduleManagementDatabaseSettings> settings) {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        
        _lessonsCollection = database.GetCollection<Lesson>(settings.Value.Collections.LessonsCollectionName);
    }
    
    public async Task<List<Lesson>> GetAsync() {
        return await _lessonsCollection.Find(lesson => true).ToListAsync();
    }
    
    public async Task<Lesson?> GetAsync(string id) {
        return await _lessonsCollection.Find<Lesson>(lesson => lesson.Id.ToString() == id).FirstOrDefaultAsync();
    }
    
    public async Task<List<Lesson>> GetLessonsByClass(string classId) {
        Console.WriteLine("Getting lessons by class ID: " + classId);
        var returnvalue = await _lessonsCollection.Find(lesson => lesson.Class.ScheduleCode == classId).ToListAsync();
        Console.WriteLine("Lessons found: " + returnvalue.Count);
        return returnvalue;
    }
}