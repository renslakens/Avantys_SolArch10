using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.CommandHandlers;
using ExamManagement.Commands;
using ExamManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ExamManagement.Services
{
    public class ExamsService
    {
        private readonly IMongoCollection<Entities.Exam> _examsCollection;

        public ExamsService(IOptions<ExamManagementDatabaseSettings> examManagementDatabaseSettings)
        {
            var client = new MongoClient(examManagementDatabaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(examManagementDatabaseSettings.Value.DatabaseName);

            _examsCollection = database.GetCollection<Entities.Exam>(examManagementDatabaseSettings.Value.Collections.ExamsCollectionName);
        }

        public async Task<List<Entities.Exam>> GetAsync()
        {
            return await _examsCollection.Find(exam => true).ToListAsync();
        }

        public async Task<Entities.Exam?> GetAsync(string id)
        {
            return await _examsCollection.Find<Entities.Exam>(exam => exam.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Entities.Exam> CreateAsync(Entities.Exam newExam)
        {
            await _examsCollection.InsertOneAsync(newExam);
            
            return newExam;
        }

        public async Task UpdateAsync(string id, Entities.Exam updatedExam)
        {
            await _examsCollection.ReplaceOneAsync(exam => exam.Id == id, updatedExam);
        }

        public async Task DeleteAsync(string id)
        {
            await _examsCollection.DeleteOneAsync(exam => exam.Id == id);
        }
    }
}