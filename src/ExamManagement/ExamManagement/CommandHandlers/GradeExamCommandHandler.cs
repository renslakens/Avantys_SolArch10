using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Commands;
using ExamManagement.Entities;

namespace ExamManagement.CommandHandlers
{
    public class GradeExamCommandHandler : IGradeExamCommandHandler
    {
        public Task<Exam> handleCommandAsync(GradeExam command)
        {
            throw new NotImplementedException();
        }
    }
}