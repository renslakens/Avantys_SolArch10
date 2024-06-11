namespace PaymentManagement {
    public class PaymentReceiverService : BackgroundService {
        private readonly PaymentConnector _paymentConnector;

        public PaymentReceiverService(PaymentConnector paymentConnector) {
            _paymentConnector = paymentConnector;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) {
            return Task.Run(() => _paymentConnector.PaymentReceiver<dynamic>(), stoppingToken);
        }
    }
}
