using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentSystemAPI.Interfaces.IServices;
using PaymentSystemAPI.Models.DTOs;

namespace PaymentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(customerId);

                if (customer == null)
                    return NotFound($"Customer with ID {customerId} not found.");

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting customer with ID {customerId}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting all customers: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerDTO customerDTO)
        {
            try
            {
                if (customerDTO == null)
                    return BadRequest("Invalid customer data");

                await _customerService.AddCustomerAsync(customerDTO);
                return Ok("Customer added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding customer: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody] CustomerDTO customerDTO)
        {
            try
            {
                await _customerService.UpdateCustomerAsync(customerId, customerDTO);
                return Ok("Customer updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating customer with ID {customerId}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(customerId);
                return Ok("Customer deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting customer with ID {customerId}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

