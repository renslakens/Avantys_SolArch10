using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PaymentManagement.Models;
using PaymentManagement.RabbitConnectors;

namespace PaymentManagement.Services {
    public class PaymentService {

        private readonly IMongoCollection<Payment> _paymentsCollection;
        private readonly PaymentConnector _paymentConnector;

        public PaymentService(IOptions<PaymentManagementDatabaseSettings> paymentManagementDatabaseSettings, PaymentConnector paymentConnector) {
            _paymentConnector = paymentConnector;

            var client = new MongoClient(paymentManagementDatabaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(paymentManagementDatabaseSettings.Value.DatabaseName);

            _paymentsCollection = database.GetCollection<Payment>(paymentManagementDatabaseSettings.Value.Collections.PaymentsCollectionName);
        }

        public async Task<List<Payment>> GetAsync() {
            return await _paymentsCollection.Find(Payment => true).ToListAsync();
        }

        public async Task<object> CreateAsync(Payment payment) {
            try {
                var filter = Builders<Payment>.Filter.Eq(payment => payment.Id, payment.Id );
                var result = await _paymentsCollection.Find(filter).FirstOrDefaultAsync();

                if (result.Id != null)
                {
                    await _paymentsCollection.InsertOneAsync(payment);
                    return payment;
                } else {
                    return new BadRequestObjectResult("Payment already exists.");
                }
            } catch (Exception e) {
                return e;
            }

        }


    }
}
