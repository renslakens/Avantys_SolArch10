using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ExamManagement.Services
{
    public class ExamsService
    {
        private readonly IMongoCollection<Exam> _examsCollection;

        public ExamsService(IOptions<ExamManagementDatabaseSettings> examManagementDatabaseSettings)
        {
            var client = new MongoClient(examManagementDatabaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(examManagementDatabaseSettings.Value.DatabaseName);

            _examsCollection = database.GetCollection<Exam>(examManagementDatabaseSettings.Value.Collections.ExamsCollectionName);
        }

        public async Task<List<Exam>> GetAsync()
        {
            return await _examsCollection.Find(exam => true).ToListAsync();
        }

        public async Task<Exam?> GetAsync(string id)
        {
            return await _examsCollection.Find<Exam>(exam => exam.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Exam> CreateAsync(Exam newExam)
        {
            await _examsCollection.InsertOneAsync(newExam);
            return newExam;
        }

        public async Task UpdateAsync(string id, Exam updatedExam)
        {
            await _examsCollection.ReplaceOneAsync(exam => exam.Id == id, updatedExam);
        }

        public async Task DeleteAsync(string id)
        {
            await _examsCollection.DeleteOneAsync(exam => exam.Id == id);
        }
    }
}