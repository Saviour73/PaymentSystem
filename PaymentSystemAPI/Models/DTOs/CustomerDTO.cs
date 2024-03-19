using System.Transactions;

namespace PaymentSystemAPI.Models.DTOs
{
    public class CustomerDTO
    {
        public string NIN { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string TransactionHistory {get;set;}

    }
}
