using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Commands;
using ExamManagement.Entities;
using ExamManagement.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace ExamManagement.CommandHandlers
{
    public class ConductExamCommandHandler : IConductExamCommandHandler
    {
        private readonly IMongoCollection<Models.Exam> _examsCollection;
        ExamConnector _examConnector = new ExamConnector();

        public ConductExamCommandHandler(IOptions<ExamManagementDatabaseSettings> examManagementDatabaseSettings)
        {
            var client = new MongoClient(examManagementDatabaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(examManagementDatabaseSettings.Value.DatabaseName);

            _examsCollection = database.GetCollection<Models.Exam>(examManagementDatabaseSettings.Value.Collections.ExamsCollectionName);
        }

        public Task<ConductExam> handleCommandAsync(ConductExam command)
        {
            Console.WriteLine("Conducting exam" + command.Id);
            var exam = _examsCollection.Find<Models.Exam>(exam => exam.Id == command.Id).FirstOrDefault();

            if (exam == null)
            {
                throw new Exception("Exam not found");
            }

            _examConnector.Send<ConductExam>("examConducted", command);

            return Task.FromResult(command);
        }

    }
}