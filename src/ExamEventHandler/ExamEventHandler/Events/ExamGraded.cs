using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Messaging;

namespace ExamManagement.Events
{
    public class ExamGraded : Event
    {
        public readonly string examId;
        public readonly string StudentId;
        public readonly double Grade;

        public ExamGraded(Guid messageId, string examId, string studentId, double grade) : base(messageId)
        {
            this.examId = examId;
            this.StudentId = studentId;
            this.Grade = grade;
        }
    }
}