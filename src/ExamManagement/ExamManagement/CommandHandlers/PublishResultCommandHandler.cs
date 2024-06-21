using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Commands;
using ExamManagement.Entities;

namespace ExamManagement.CommandHandlers
{
    public class PublishResultCommandHandler : IPublishResultCommandHandler
    {
        public Task<Exam> handleCommandAsync(PublishResult command)
        {
            throw new NotImplementedException();
        }
    }
}