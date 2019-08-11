using System.Threading.Tasks;

namespace CarFinance.Api.Customer.Data
{
    public interface ICustomerDb
    {
        Task<Models.Customer> Insert(Models.Customer customer);
    }
}