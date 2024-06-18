using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PaymentManagement.Models;

namespace PaymentManagement.Services
{
    public class PermissionService
    {

        private readonly IMongoCollection<Permissions> _permissionsCollection;

        public PermissionService(IOptions<PaymentManagementDatabaseSettings> examManagementDatabaseSettings)
        {
            var client = new MongoClient(examManagementDatabaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(examManagementDatabaseSettings.Value.DatabaseName);

            _permissionsCollection = database.GetCollection<Permissions>(examManagementDatabaseSettings.Value.Collections.PermissionsCollectionName);
        }

        public async Task<List<Permissions>> GetAsync()
        {
            return await _permissionsCollection.Find(Permissions => true).ToListAsync();
        }
    }
}
