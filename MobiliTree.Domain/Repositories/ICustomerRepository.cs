using MobiliTree.Domain.Models;

namespace MobiliTree.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(string customerId);
    }
}