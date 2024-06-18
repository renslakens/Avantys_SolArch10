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

            _paymentsCollection = database.GetCollection<Payment>(paymentManagementDatabaseSettings.Value.Collections.PermissionsCollectionName);
        }

        public async Task<List<Payment>> GetAsync() {
            return await _paymentsCollection.Find(Payment => true).ToListAsync();
        }

        public async Task<object> CreateAsync(Payment payment) {
            try {
                await _paymentsCollection.InsertOneAsync(payment);
                return payment;
            } catch (Exception e) {
                return e;
            }

        }


    }
}
