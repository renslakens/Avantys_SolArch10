using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Commands;
using ExamManagement.Entities;

namespace ExamManagement.CommandHandlers
{
    public interface IConductExamCommandHandler
    {
        Task<ConductExam> handleCommandAsync(ConductExam command);
    }
}