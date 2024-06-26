using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ScheduleManagement.Models;

namespace ScheduleManagement.Services;

public class ClassService {
    private readonly IMongoCollection<Class> _classesCollection;
    
    public ClassService(IOptions<ScheduleManagementDatabaseSettings> settings) {
        var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_ADDRESS"));
        var database = client.GetDatabase(settings.Value.DatabaseName);
        
        _classesCollection = database.GetCollection<Class>(settings.Value.Collections.ClassesCollectionName);
    }
    
    public async Task<List<Class>> GetAsync() {
        return await _classesCollection.Find(_class => true).ToListAsync();
    }
    
    public async Task<Class?> GetAsync(string id) {
        return await _classesCollection.Find(_class => _class.Id.ToString() == id).FirstOrDefaultAsync();
    }
    
    public async Task<Class> GetClassByScheduleCode(string scheduleCode) {
        return await _classesCollection.Find(_class => _class.ScheduleCode == scheduleCode).FirstOrDefaultAsync();
    }
}