using MobiliTree.Domain.Models;
using MobiliTree.Domain.Repositories;

namespace MobiliTree.FakeData.Repositories
{
    public class CustomerRepositoryFake : ICustomerRepository
    {
        private readonly Dictionary<string, Customer> _customers;

        public CustomerRepositoryFake(Dictionary<string, Customer> customers)
        {
            _customers = customers;
        }

        public Customer GetCustomer(string customerId)
        {
            if (_customers.TryGetValue(customerId, out var customer))
            {
                return customer;    
            }

            return null;
        }
    }
}
