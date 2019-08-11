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
    }
}