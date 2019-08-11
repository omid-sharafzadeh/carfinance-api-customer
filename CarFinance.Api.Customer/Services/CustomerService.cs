using System.Collections.Generic;
using System.Threading.Tasks;
using CarFinance.Api.Customer.Data;

namespace CarFinance.Api.Customer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDb _customerDb;

        public CustomerService(ICustomerDb customerDb)
        {
            _customerDb = customerDb;
        }
        
        public async Task<Models.Customer> Add(Models.Customer newCustomer)
        {
            return await _customerDb.Insert(newCustomer);
        }

        public async Task<Models.Customer> GetById(string customerId)
        {
            return await _customerDb.GetById(customerId);
        }

        public async Task<IEnumerable<Models.Customer>> GetAll()
        {
            return await _customerDb.GetAll();
        }

        public async Task Update(Models.Customer updatedCustomer)
        { 
            await _customerDb.Update(updatedCustomer);
        }

        public async Task Delete(Models.Customer customerToBeDeleted)
        {
            await _customerDb.Delete(customerToBeDeleted);
        }
    }
}