using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamManagement.Entities
{
    public class Exam : Entity<string>
    {
        public string StudentId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public Module Module { get; set; }
        public double? Grade { get; set; }
        public DateTime? PublishedDate { get; set; }

        public Exam(string id) : base(id)
        {
        }

        public Exam() : base(Guid.NewGuid().ToString()) // Parameterless constructor for deserialization
        {
        }

        public void Schedule(string studentId, DateTime scheduledDate, Module module)
        {
            this.StudentId = studentId;
            this.ScheduledDate = scheduledDate;
            this.Module = module;
        }

        // tostring
        public override string ToString()
        {
            return "Exam{" +
                   "StudentId='" + StudentId + '\'' +
                   ", scheduledDate=" + ScheduledDate +
                   ", module=" + Module +
                   '}';
        }

    }
}