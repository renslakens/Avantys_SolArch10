using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Messaging;
using ExamManagement.Entities;

namespace ExamManagement.Commands
{
    public class ScheduleExam : Command
    {
        public string examId;
        public string studentId;
        public DateTime scheduledDate;
        public Module module;

        // public ScheduleExam() : base(Guid.NewGuid())
        // {
        // }

        public ScheduleExam(Guid messageId, string examId, string studentId, DateTime scheduledDate, Module module) : base(messageId)
        {
            this.examId = examId;
            this.studentId = studentId;
            this.scheduledDate = scheduledDate;
            this.module = module;
        }
    }
}