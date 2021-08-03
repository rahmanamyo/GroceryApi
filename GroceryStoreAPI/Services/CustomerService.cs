using System.Collections.Generic;
using System.Linq;
using GroceryStoreAPI.Domain;
using GroceryStoreAPI.Repository;

namespace GroceryStoreAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly List<Customer> _customers;

        public CustomerService(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
            _customers = _repositoryProvider.ReadCustomers();
        }

        public List<Customer> GetCustomers()
        {
            return _customers;
        }

        public Customer GetCustomerById(int id)
        {
            return _customers.SingleOrDefault(x => x.Id == id);
        }

        public bool UpdateCustomer(Customer customerToUpdate)
        {
            var exists = GetCustomerById(customerToUpdate.Id) != null;

            if (!exists)
                return false;

            var index = _customers.FindIndex(x => x.Id == customerToUpdate.Id);
            _customers[index] = customerToUpdate;
            _repositoryProvider.WriteCustomers(_customers);
            return true;
        }
        
        public bool DeleteCustomer(int id)
        {
            var customer = GetCustomerById(id);

            if (customer == null)
                return false;

            _customers.Remove(customer);
            _repositoryProvider.WriteCustomers(_customers);
            return true;
        }

        public void AddCustomer(Customer customerToAdd)
        {
            var maxId = _customers.Max(c => c.Id);
            customerToAdd.Id = maxId + 1;
            _customers.Add(customerToAdd);
            _repositoryProvider.WriteCustomers(_customers);
        }

    }
}