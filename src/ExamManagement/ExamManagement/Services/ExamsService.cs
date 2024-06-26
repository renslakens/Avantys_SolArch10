using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.CommandHandlers;
using ExamManagement.Commands;
using ExamManagement.Entities;
using ExamManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ExamManagement.Services
{
    public class ExamsService
    {
        private readonly IMongoCollection<Models.Exam> _examsCollection;
        private readonly IScheduleExamCommandHandler _scheduleExamCommandHandler;
        private readonly IConductExamCommandHandler _conductExamCommandHandler;
        private readonly IGradeExamCommandHandler _gradeExamCommandHandler;
        private readonly IPublishResultCommandHandler _publishExamResultCommandHandler;


        public ExamsService(IOptions<ExamManagementDatabaseSettings> examManagementDatabaseSettings, IScheduleExamCommandHandler scheduleExamCommandHandler, IConductExamCommandHandler conductExamCommandHandler, IGradeExamCommandHandler gradeExamCommandHandler, IPublishResultCommandHandler publishResultCommandHandler)
        {
            var client = new MongoClient(examManagementDatabaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(examManagementDatabaseSettings.Value.DatabaseName);

            _examsCollection = database.GetCollection<Models.Exam>(examManagementDatabaseSettings.Value.Collections.ExamsCollectionName);

            _scheduleExamCommandHandler = scheduleExamCommandHandler ?? throw new ArgumentNullException(nameof(scheduleExamCommandHandler));
            _conductExamCommandHandler = conductExamCommandHandler ?? throw new ArgumentNullException(nameof(conductExamCommandHandler));
            _gradeExamCommandHandler = gradeExamCommandHandler ?? throw new ArgumentNullException(nameof(gradeExamCommandHandler));
            _publishExamResultCommandHandler = publishResultCommandHandler ?? throw new ArgumentNullException(nameof(publishResultCommandHandler));
        }

        public async Task<List<Models.Exam>> GetAsync()
        {
            return await _examsCollection.Find(exam => true).ToListAsync();
        }

        public async Task<Models.Exam?> GetAsync(string id)
        {
            return await _examsCollection.Find<Models.Exam>(exam => exam.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ScheduleExam> ScheduleExamAsync(ScheduleExam command)
        {
            try
            {
                ScheduleExam exam = await _scheduleExamCommandHandler.handleCommandAsync(command);

                Console.WriteLine("Exam scheduled" + exam);
                return exam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ConductExam> ConductExamAsync(ConductExam command)
        {
            try
            {
                Console.WriteLine("Conducting exam in service " + command.Id);
                var exam = await _conductExamCommandHandler.handleCommandAsync(command);

                Console.WriteLine("Exam conducted" + exam);
                return exam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GradeExam> GradeExamAsync(GradeExam command)
        {
            try
            {
                Console.WriteLine("Yeeehaw");
                var exam = await _gradeExamCommandHandler.handleCommandAsync(command);

                Console.WriteLine("Exam graded" + exam);
                return exam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PublishResult> PublishExamResultAsync(PublishResult command)
        {
            try
            {
                var exam = await _publishExamResultCommandHandler.handleCommandAsync(command);

                Console.WriteLine("Exam result published" + exam);
                return exam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // public async Task UpdateAsync(string id, Entities.Exam updatedExam)
        // {
        //     await _examsCollection.ReplaceOneAsync(exam => exam.Id == id, updatedExam);
        // }

        // public async Task DeleteAsync(string id)
        // {
        //     await _examsCollection.DeleteOneAsync(exam => exam.Id == id);
        // }
    }
}