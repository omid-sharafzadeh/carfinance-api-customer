using System.Collections.Generic;
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
            var customer = new Models.Customer(
                newCustomer.Email, 
                newCustomer.FirstName, 
                newCustomer.Surname, 
                newCustomer.Password);
            
            await _customers.InsertOneAsync(customer);
            return customer;
        }

        public async Task<Models.Customer> GetById(string id)
        {
            var customer = await _customers.FindAsync(c => c.Id.Equals(id));
            return customer.FirstOrDefault();
        }

        public async Task<IEnumerable<Models.Customer>> GetAll()
        {
            var customers = await _customers.FindAsync(c => true);
            return customers.ToList();
        }

        public async Task Update(Models.Customer updatedCustomer)
        { 
            await _customers.ReplaceOneAsync(c => c.Id == updatedCustomer.Id, updatedCustomer);
        }

        public async Task Delete(Models.Customer customerToBeDeleted)
        {
            await _customers.DeleteOneAsync(c => c.Id == customerToBeDeleted.Id);
        }
    }
}