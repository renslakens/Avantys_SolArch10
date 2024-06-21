using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ExamManagement.Messaging;

namespace ExamManagement.Events
{
    public class ExamScheduled : Event
    {
        public readonly string examId;
        public readonly string StudentId;
        public readonly DateTime ScheduledDate;
        public readonly Module module;

        public ExamScheduled(Guid messageId, string examId, string studentId, DateTime scheduledDate, Module module) : base(messageId)
        {
            this.examId = examId;
            this.StudentId = studentId;
            this.ScheduledDate = scheduledDate;
            this.module = module;
        }

    }
}