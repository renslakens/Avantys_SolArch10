﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ScheduleManagement.Models;

namespace ScheduleManagement.Services;

public class ScheduleService {
    private readonly IMongoCollection<Schedule> _schedulesCollection;
    
    public ScheduleService(IOptions<ScheduleManagementDatabaseSettings> settings) {
        var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_ADDRESS"));
        var database = client.GetDatabase(settings.Value.DatabaseName);
        
        _schedulesCollection = database.GetCollection<Schedule>(settings.Value.Collections.SchedulesCollectionName);
    }
    
    public async Task<List<Schedule>> GetAsync() {
        return await _schedulesCollection.Find(schedule => true).ToListAsync();
    }
    
    public async Task<Schedule?> GetAsync(string id) {
        return await _schedulesCollection.Find<Schedule>(schedule => schedule.Id.ToString() == id).FirstOrDefaultAsync();
    }
    
    public async Task<Schedule> CreateAsync(Schedule newSchedule) {
        await _schedulesCollection.InsertOneAsync(newSchedule);
        return newSchedule;
    }
    
    public async Task UpdateAsync(string id, Schedule updatedSchedule) {
        await _schedulesCollection.ReplaceOneAsync(schedule => schedule.Id.ToString() == id, updatedSchedule);
    }
    
    public async Task DeleteAsync(string id) {
        await _schedulesCollection.DeleteOneAsync(schedule => schedule.Id.ToString() == id);
    }
}