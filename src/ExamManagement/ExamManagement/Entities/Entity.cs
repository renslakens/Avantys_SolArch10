using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamManagement.Entities
{
    public class Entity <TId>
    {
        public TId Id { get; set; }

        public Entity(TId id)
        {
            Id = id;
        }
    }
}