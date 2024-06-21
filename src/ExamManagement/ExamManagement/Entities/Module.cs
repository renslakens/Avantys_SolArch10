using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExamManagement.Entities
{
    public class Module : Entity<string>
    {
        public string name { get; set; }
        
        [JsonConstructor]
        public Module(string id, string name) : base(id)
        {
            this.name = name;
        }

        public Module(string id) : base(id)
        {
        }
    }
}