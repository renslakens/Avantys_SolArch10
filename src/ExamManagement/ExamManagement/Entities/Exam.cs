using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamManagement.Entities
{
    public class Exam : Entity<string>
    {
        public  string StudentId;
        public  DateTime scheduledDate;
        public  Module module;

        public Exam(string id) : base(id)
        {
        }

        public void Schedule(string studentId, DateTime scheduledDate, Module module)
        {
            this.StudentId = studentId;
            this.scheduledDate = scheduledDate;
            this.module = module;
        }

    }
}