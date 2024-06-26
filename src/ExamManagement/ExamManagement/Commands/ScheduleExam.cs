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
        public string? Id { get; set; }
        public string studentId { get; set; }
        public DateTime scheduledDate { get; set; }
        public Module? module { get; set; }
        public Proctor? proctor { get; set; } 
        public ScheduleExam() : base(Guid.NewGuid())
        {
        }

        
        public ScheduleExam(Guid messageId, string examId, string studentId, DateTime scheduledDate, Module module, Proctor Procotor) : base(messageId)
        {
            this.Id = examId;
            this.studentId = studentId;
            this.scheduledDate = scheduledDate;
            this.module = module;
            this.proctor = Procotor;
        }
    }
}