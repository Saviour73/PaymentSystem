using System.ComponentModel.DataAnnotations;

namespace PaymentSystemAPI.Models.DomainModel
{
    public class Merchant
    {
        public int Id { get; set; }
        [Required]
        public string BusinessId { get; set; }
        [Required]
        public string BusinessName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SurName { get; set; }

        public DateTime DateOfEstablisment { get; set; }
        [Required]
        [StringLength(11, ErrorMessage = "Phone Number must have a maximum length of 11")]
        [RegularExpression("^(080|081|070|091|090)[0-9]*$", ErrorMessage = "Phone Number must follow righr format and must be 11 dights.")]
        public string PhoneNumber { get; set; }

        public double AverageTransactionVolume { get; set; }
    }
}
