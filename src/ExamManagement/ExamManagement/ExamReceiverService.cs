using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamManagement
{
    public class ExamReceiverService : BackgroundService
    {
        private readonly ExamConnector _examConnector;

        public ExamReceiverService(ExamConnector examConnector)
        {
            _examConnector = examConnector;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => _examConnector.ReceiveExam<dynamic>(), stoppingToken);
        }
    }
}