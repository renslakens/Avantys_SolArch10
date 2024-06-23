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
        public ExamConnector ExamConnector = new();

        public Task<Exam> handleCommandAsync(ScheduleExam command)
        {
            Console.WriteLine("Handling ScheduleExam command with messageID: " + command.MessageId);
            Console.WriteLine("Scheduling exam for student " + command.studentId + " on " + command.scheduledDate);
            Exam exam = new Exam(command.examId);

            exam.Schedule(command.studentId, command.scheduledDate, command.module);

            ExamConnector.Send<Exam>("ScheduleExam" ,exam);

            return Task.FromResult(exam);
        }
    }
}