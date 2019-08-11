using System.Threading.Tasks;
using MongoDB.Driver;

namespace CarFinance.Api.Customer.Data
{
    public class CustomerDb : ICustomerDb
    {
        private readonly IMongoCollection<Models.Customer> _customers;
        
        public CustomerDb(ICustomerDbSettings customerDbSettings)
        {
            var client = new MongoClient(customerDbSettings.ConnectionString);
            var database = client.GetDatabase(customerDbSettings.DatabaseName);
            _customers = database.GetCollection<Models.Customer>(customerDbSettings.CollectionName);
        }
        
        public async Task<Models.Customer> Insert(Models.Customer newCustomer)
        {
            var customer = new Models.Customer(newCustomer.Email);
            await _customers.InsertOneAsync(customer);
            return customer;
        }

        public async Task<Models.Customer> GetById(string id)
        {
            var customer = await _customers.FindAsync(c => c.Id.Equals(id));
            return customer.FirstOrDefault();
        }
    }
}