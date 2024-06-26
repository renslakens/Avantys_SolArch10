using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ExamManagement.Messaging
{
    public class Event : Message
    {
        public Event()
        {
        }

        public Event(ObjectId messageId) : base(messageId)
        {
        }

        public Event(string messageType) : base(messageType)
        {
        }

        public Event(ObjectId messageId, string messageType) : base(messageId, messageType)
        {
        }
    }
}