using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Messaging;

namespace ExamManagement.Commands
{
    public class ConductExam : Command
    {
        public string Id { get; set; }

        public ConductExam() : base(Guid.NewGuid())
        {
        }
        public ConductExam(Guid messageId, string examId) : base(messageId)
        {
            this.Id = examId;
        }
    }
}