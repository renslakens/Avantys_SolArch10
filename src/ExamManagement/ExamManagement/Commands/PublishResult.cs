using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Messaging;

namespace ExamManagement.Commands
{
    public class PublishResult : Command
    {
        public string Id { get; set; }
        public DateTime PublishedDate = DateTime.Now;

        public PublishResult() : base(Guid.NewGuid())
        {
        }

        public PublishResult(Guid messageId, string examId) : base(messageId)
        {
            this.Id = examId;
        }
    }
}