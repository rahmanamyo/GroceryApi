using System;
using GroceryStoreAPI.Contracts.v1;
using GroceryStoreAPI.Contracts.v1.Requests;
using GroceryStoreAPI.Contracts.v1.Responses;
using GroceryStoreAPI.Domain;
using GroceryStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreAPI.Controllers.v1
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// updates a customer 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut(ApiRoutes.Customers.Update)]
        public IActionResult Update([FromRoute] int customerId, [FromBody] UpdateCustomerRequest request)
        {
            var customer = new Customer
            {
                Id = customerId,
                Name = request.Name
            };
            
            var updated = _customerService.UpdateCustomer(customer);

            if (updated)
                return Ok(customer);

            return NotFound();
        }
        
        /// <summary>
        /// returns a customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Customers.Get)]
        public IActionResult Get([FromRoute] int customerId)
        {
            throw new Exception("test");
            var customer = _customerService.GetCustomerById(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        /// <summary>
        /// returns all Customers
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Customers.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_customerService.GetCustomers());
        }

        /// <summary>
        /// creates a customer
        /// </summary>
        /// <param name="createCustomerRequest"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Customers.Create)]
        public IActionResult Create([FromBody] CreateCustomerRequest createCustomerRequest)
        {
            var customer = new Customer
            {
                Name = createCustomerRequest.Name
            };
         
            _customerService.AddCustomer(customer);
            
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Customers.Get.Replace("{customerId}", customer.Id.ToString());

            var response = new CustomerResponse {Id = customer.Id, Name = customer.Name};
            return Created(locationUri, response);
        }

        /// <summary>
        /// deletes a customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete(ApiRoutes.Customers.Delete)]
        public IActionResult Delete([FromRoute] int customerId)
        {
            var deleted = _customerService.DeleteCustomer(customerId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
