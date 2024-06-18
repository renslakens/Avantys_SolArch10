using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PaymentManagement.Models;

namespace PaymentManagement.Services
{
    public class PermissionService
    {

        private readonly IMongoCollection<Permission> _permissionsCollection;

        public PermissionService(IOptions<PaymentManagementDatabaseSettings> paymentManagementDatabaseSettings)
        {
            var client = new MongoClient(paymentManagementDatabaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(paymentManagementDatabaseSettings.Value.DatabaseName);

            _permissionsCollection = database.GetCollection<Permission>(paymentManagementDatabaseSettings.Value.Collections.PermissionsCollectionName);
        }

        public async Task<List<Permission>> GetAsync()
        {
            return await _permissionsCollection.Find(Permissions => true).ToListAsync();
        }
    }
}
