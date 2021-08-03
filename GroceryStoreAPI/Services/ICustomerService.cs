using System.Collections.Generic;
using GroceryStoreAPI.Domain;

namespace GroceryStoreAPI.Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();
        Customer GetCustomerById(int id);
        bool UpdateCustomer(Customer customerToUpdate);
        bool DeleteCustomer(int id);
        void AddCustomer(Customer customerToAdd);
    }
}