using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarFinance.Api.Customer.Services
{
    public interface ICustomerService
    {
        Task<Models.Customer> Add(Models.Customer newCustomer);
        Task<Models.Customer> GetById(string customerId);
        Task<IEnumerable<Models.Customer>> GetAll();
        Task Update(Models.Customer updatedCustomer);
        Task Delete(Models.Customer customerToBeDeleted);
    }
}