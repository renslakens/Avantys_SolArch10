using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ExamManagement.Messaging
{
    public class Message {
    public readonly ObjectId MessageId;
    public readonly string MessageType;

    public Message() : this(ObjectId.GenerateNewId())
    {
    }

    public Message(ObjectId messageId)
    {
        MessageId = messageId;
        MessageType = this.GetType().Name;
    }

    public Message(string messageType) : this(ObjectId.GenerateNewId())
    {
        MessageType = messageType;
    }

    public Message(ObjectId messageId, string messageType)
    {
        MessageId = messageId;
        MessageType = messageType;
    }
}
}