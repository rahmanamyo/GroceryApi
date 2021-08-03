using System.Collections.Generic;
using GroceryStoreAPI.Domain;

namespace GroceryStoreAPI.Repository
{
    public interface IRepositoryProvider
    {
        List<Customer> ReadCustomers();
        void WriteCustomers(List<Customer> customers);
    }
}