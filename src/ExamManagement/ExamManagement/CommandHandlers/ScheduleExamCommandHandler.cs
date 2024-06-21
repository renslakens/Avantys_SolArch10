using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Commands;
using ExamManagement.Entities;

namespace ExamManagement.CommandHandlers
{
    public class ScheduleExamCommandHandler : IScheduleExamCommandHandler
    {
        public ExamConnector ExamConnector = new ExamConnector();

        public Task<Exam> handleCommandAsync(ScheduleExam command)
        {
            Console.WriteLine("Handling ScheduleExam command" + command);
            Console.WriteLine("Handling ScheduleExam command" + command.studentId + " " + command.scheduledDate + " " + command.module.name);
            Exam exam = new Exam(command.examId);

            exam.Schedule(command.studentId, command.scheduledDate, command.module);

            Console.WriteLine("Exam scheduled" + exam.Id + " " + exam.StudentId + " " + exam.scheduledDate + " " + exam.module.name);

            ExamConnector.Send<Exam>("ScheduleExam" ,exam);

            return Task.FromResult(exam);
        }
    }
}