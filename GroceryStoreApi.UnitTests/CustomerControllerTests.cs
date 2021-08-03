using System.Collections.Generic;
using FakeItEasy;
using GroceryStoreAPI.Contracts.v1.Requests;
using GroceryStoreAPI.Controllers.v1;
using GroceryStoreAPI.Domain;
using GroceryStoreAPI.Repository;
using GroceryStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GroceryStoreApi.UnitTests
{
    public class CustomerControllerTests
    {
        [Fact]
        public void CustomerController_Returns_AllCustomers()
        {
            //Arrange
            var fakeCustomerData = new List<Customer>
            {
                new() {Id = 1, Name = "user 1"}, 
                new() {Id = 2, Name = "user 2"}
            };
            
            var mockCustomerService = A.Fake<ICustomerService>();
            A.CallTo(() => mockCustomerService.GetCustomers()).Returns(fakeCustomerData);
        
            var controller = new CustomerController(mockCustomerService);
            
            //Act
            var okResult = controller.GetAll() as OkObjectResult;
            
            //Assert
            var customers = okResult?.Value as List<Customer>;
            
            Assert.NotNull(customers);
            Assert.NotEmpty(customers);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(2, customers.Count);
        }
        
        [Fact]
        public void CustomerController_Returns_CustomerById()
        {
            //Arrange
            var fakeTargetCustomer = new Customer {Id = 2, Name = "user 2"};
            
            var mockCustomerService = A.Fake<ICustomerService>();
            A.CallTo(() => mockCustomerService.GetCustomerById(2)).Returns(fakeTargetCustomer);
        
            var controller = new CustomerController(mockCustomerService);
            
            //Act
            var okResult = controller.Get(2) as OkObjectResult;
            
            //Assert
            var customer = okResult?.Value as Customer;
            
            Assert.NotNull(customer);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(2, customer.Id);
            Assert.Equal("user 2", customer.Name);
        }

        [Fact]
        public void CustomerController_Updates_Customer()
        {
            //Arrange
            var fakeCustomerData = new List<Customer>
            {
                new() {Id = 1, Name = "user 1"}, 
                new() {Id = 2, Name = "user 2"}
            };

            var mockRepoService = A.Fake<IRepositoryProvider>();
            A.CallTo(() => mockRepoService.ReadCustomers()).Returns(fakeCustomerData);

            var customerService = new CustomerService(mockRepoService);
            var controller = new CustomerController(customerService);

            var updateCustomerRequest = new UpdateCustomerRequest
            {
                   Name = "updated user name"
            };
            
            //Act
            controller.Update(1, updateCustomerRequest);
            var okResult = controller.Get(1) as OkObjectResult;
            
            //Assert
            var customer = okResult?.Value as Customer;
            
            Assert.NotNull(customer);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(1, customer.Id);
            Assert.Equal("updated user name", customer.Name);
        }
    }
}