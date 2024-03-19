using AutoMapper;
using PaymentSystemAPI.Interfaces.IRespository;
using PaymentSystemAPI.Interfaces.IServices;
using PaymentSystemAPI.Models.DomainModel;
using PaymentSystemAPI.Models.DTOs;

namespace PaymentSystemAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IBaseRespository<Customer> _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IBaseRespository<Customer> customerRepository, IMapper mapper, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
            {
                _logger.LogWarning($"Customer with ID {customerId} not found.");
                return null;
            }

            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
        }

        public async Task<bool> AddCustomerAsync(CustomerDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            if (customer == null)
            {
                return false;

            }
            var Customer = new Customer
            {
                NIN = customerDTO.NIN,
                FirstName = customerDTO.FirstName,
                SurName = customerDTO.SurName,
                PhoneNumber = customerDTO.PhoneNumber,
                DateOfBirth = customerDTO.DateOfBirth,
                TransactionHistory = customerDTO.TransactionHistory
                
            };
            await _customerRepository.AddAsync(customer);

            return true;
        }

        public async Task UpdateCustomerAsync(int customerId, CustomerDTO customerDTO)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(customerId);

            if (existingCustomer == null)
            {
                _logger.LogWarning($"Customer with ID {customerId} not found.");
                return;
            }

            _mapper.Map(customerDTO, existingCustomer);
            await _customerRepository.UpdateAsync(existingCustomer);
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
            {
                _logger.LogWarning($"Customer with ID {customerId} not found.");
                return;
            }

            await _customerRepository.DeleteAsync(customer);
        }
    }
}
