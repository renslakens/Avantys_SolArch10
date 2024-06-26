using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExamManagement.Entities
{
    public class Proctor : Entity<string>
    {
        
        public string Name { get; set; }

        [JsonConstructor]
        public Proctor(string id, string name) : base(id)
        {
            this.Name = name;
        }

        public Proctor() : base(Guid.NewGuid().ToString()) // Parameterless constructor for deserialization
        {
        }

        public Proctor(string id) : base(id)
        {
        }
    }
}