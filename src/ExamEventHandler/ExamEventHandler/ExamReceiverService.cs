using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace ExamManagement
{
    public class ExamReceiverService : IHostedService
    {
        private readonly ExamConnector _examConnector;

        public ExamReceiverService(ExamConnector examConnector)
        {
            _examConnector = examConnector;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => _examConnector.Receive<dynamic>(), cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}