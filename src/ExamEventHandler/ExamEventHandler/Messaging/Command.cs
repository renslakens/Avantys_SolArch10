using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ExamManagement.Messaging
{
    public class Command : Message
    {
        public Command()
        {
        }

        public Command(ObjectId messageId) : base(messageId)
        {
        }

        public Command(string messageType) : base(messageType)
        {
        }

        public Command(ObjectId messageId, string messageType) : base(messageId, messageType)
        {
        }
    }
}