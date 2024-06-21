using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Commands;
using ExamManagement.Entities;

namespace ExamManagement.CommandHandlers
{
    public class ConductExamCommandHandler : IConductExamCommandHandler
    {
        ExamConnector _examConnector = new ExamConnector();

        public Task<Exam> handleCommandAsync(ConductExam command)
        {
            throw new NotImplementedException();
        }
    }
}