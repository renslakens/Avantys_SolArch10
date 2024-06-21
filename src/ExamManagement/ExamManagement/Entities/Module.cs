using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamManagement.Entities
{
    public class Module : Entity<string>
    {
        public string name;

        public Module(string id, string name) : base(id)
        {
            this.name = name;
        }

        public Module(string id) : base(id)
        {
        }
    }
}