using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamManagement.Entities
{
    public class Exam : Entity<string>
    {
        public  string StudentId { get; set; }
        public  DateTime scheduledDate { get; set; }
        public  Module module { get; set; }

        public Exam(string id) : base(id)
        {
        }

        public Exam() : base(Guid.NewGuid().ToString()) // Parameterless constructor for deserialization
{
        }

        public void Schedule(string studentId, DateTime scheduledDate, Module module)
        {
            this.StudentId = studentId;
            this.scheduledDate = scheduledDate;
            this.module = module;
        }

        // tostring
        public override string ToString() {
            return "Exam{" +
                   "StudentId='" + StudentId + '\'' +
                   ", scheduledDate=" + scheduledDate +
                   ", module=" + module +
                   '}';
        }

    }
}