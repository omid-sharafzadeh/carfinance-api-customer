using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarFinance.Api.Customer.Data
{
    public interface ICustomerDb
    {
        Task<Models.Customer> Insert(Models.Customer customer);
        Task<Models.Customer> GetById(string id);
        Task<IEnumerable<Models.Customer>> GetAll();
    }
}