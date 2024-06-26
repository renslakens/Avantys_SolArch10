using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Commands;
using ExamManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ExamManagement.CommandHandlers
{
    public class GradeExamCommandHandler : IGradeExamCommandHandler
    {
        private readonly IMongoCollection<Exam> _examsCollection;
        ExamConnector _examConnector = new ExamConnector();

        public GradeExamCommandHandler(IOptions<ExamManagementDatabaseSettings> examManagementDatabaseSettings)
        {
            var client = new MongoClient(examManagementDatabaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(examManagementDatabaseSettings.Value.DatabaseName);

            _examsCollection = database.GetCollection<Models.Exam>(examManagementDatabaseSettings.Value.Collections.ExamsCollectionName);
        }
        public Task<GradeExam> handleCommandAsync(GradeExam command)
        {
            var exam = _examsCollection.Find<Exam>(exam => exam.Id == command.Id).FirstOrDefault();
            
            if (exam == null)
            {
                throw new Exception("Exam not found");
            }
            _examConnector.Send<GradeExam>("examGraded", command);

            return Task.FromResult(command);
        }
    }
}