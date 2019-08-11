namespace CarFinance.Api.Customer.Data
{
    public class CustomerDbSettings : ICustomerDbSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}