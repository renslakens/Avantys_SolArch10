using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Commands;
using ExamManagement.Entities;
using MongoDB.Bson;

namespace ExamManagement.CommandHandlers
{
    public class ScheduleExamCommandHandler : IScheduleExamCommandHandler
    {
        public ExamConnector ExamConnector = new ExamConnector();

        public Task<ScheduleExam> handleCommandAsync(ScheduleExam command)
        {
            // Exam exam = new Exam(command.examId);

            // exam.Schedule(command.studentId, command.scheduledDate, command.module);

            // Console.WriteLine("Exam scheduled" + exam.Id + " " + exam.StudentId + " " + exam.scheduledDate + " " + exam.module.name);

            // ExamConnector.Send<Exam>("ScheduleExam" ,exam);
            command.Id = ObjectId.GenerateNewId().ToString();
            Console.WriteLine("IDDDD " + command.Id);
            ExamConnector.Send<ScheduleExam>("examScheduled", command);
            return Task.FromResult(command);

            // return Task.FromResult(exam);
        }
    }
}