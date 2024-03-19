using AutoMapper;
using PaymentSystemAPI.Models.DomainModel;
using PaymentSystemAPI.Models.DTOs;

namespace PaymentSystemAPI.Profiles.Automappings
{
    public class PaymentSystemMappings : Profile
    {
        public PaymentSystemMappings()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();

            CreateMap<Merchant, MerchantDTO>();
            CreateMap<MerchantDTO, Merchant>();

        }
   
    }

}
