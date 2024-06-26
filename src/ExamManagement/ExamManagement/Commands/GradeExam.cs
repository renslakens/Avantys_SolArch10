using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Messaging;

namespace ExamManagement.Commands
{
    public class GradeExam : Command
    {
        public string Id { get; set; }
        public double Grade { get; set; }

        public GradeExam() : base(Guid.NewGuid())
        {
        }

        public GradeExam(Guid messageId, string examId, double grade) : base(messageId)
        {
            this.Id = examId;
            this.Grade = grade;
        }
    }
}