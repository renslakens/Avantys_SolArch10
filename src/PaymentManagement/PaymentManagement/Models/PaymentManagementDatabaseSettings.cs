namespace PaymentManagement.Models {
    public class PaymentManagementDatabaseSettings {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public CollectionsSettings Collections { get; set; } = null!;
    }

    public class CollectionsSettings {
        public string PermissionsCollectionName { get; set; } = null!;
        public string PaymentsCollectionName { get; set; } = null!;
    }
}
