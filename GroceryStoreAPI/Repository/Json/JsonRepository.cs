using System;
using System.Collections.Generic;
using System.IO;
using GroceryStoreAPI.Domain;
using Newtonsoft.Json;

namespace GroceryStoreAPI.Repository.Json
{
    public class JsonRepository : IRepositoryProvider
    {
        private string DatabasePath => Path.Combine(AppContext.BaseDirectory, "Repository", "Json", "database.json");
        
        public List<Customer> ReadCustomers()
        {
            if (!File.Exists(DatabasePath)) return new List<Customer>();
            var content = File.ReadAllText(DatabasePath);
            var customers = JsonConvert.DeserializeObject<CustomersJson>(content);
            return customers?.Customers;
        }

        public void WriteCustomers(List<Customer> customers)
        {
            var customersJson = new CustomersJson {Customers = customers};
            using var file = File.CreateText(DatabasePath);
            var serializer = new JsonSerializer();
            serializer.Serialize(file, customersJson);
        }
    }
}