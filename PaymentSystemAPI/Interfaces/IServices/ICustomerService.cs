using PaymentSystemAPI.Models.DTOs;

namespace PaymentSystemAPI.Interfaces.IServices
{
    public interface ICustomerService
    {
        Task<CustomerDTO> GetCustomerByIdAsync(int customerId);
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
        Task<bool> AddCustomerAsync(CustomerDTO customerDTO);
        Task UpdateCustomerAsync(int customerId, CustomerDTO customerDTO);
        Task DeleteCustomerAsync(int customerId);
    }
}
