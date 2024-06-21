using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Messaging;

namespace ExamManagement.Commands
{
    public class PublishResult : Command
    {
        public readonly string examId;
        public readonly string StudentId;
        public readonly double Grade;
        public readonly DateTime PublishedDate;

        public PublishResult(Guid messageId, string examId, string studentId, double grade, DateTime publishedDate) : base(messageId)
        {
            this.examId = examId;
            this.StudentId = studentId;
            this.Grade = grade;
            this.PublishedDate = publishedDate;
        }
    }
}