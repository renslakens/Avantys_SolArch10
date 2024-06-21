using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Messaging;

namespace ExamManagement.Events
{
    public class ExamConducted : Event
    {
        public readonly string examId;
        public readonly string StudentId;
        public readonly DateTime ConductedDate;

        public ExamConducted(Guid messageId, string examId, string studentId, DateTime conductedDate) : base(messageId)
        {
            this.examId = examId;
            this.StudentId = studentId;
            this.ConductedDate = conductedDate;
        }
    }
}