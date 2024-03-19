using AutoMapper;
using PaymentSystemAPI.Interfaces.IRespository;
using PaymentSystemAPI.Interfaces.IServices;
using PaymentSystemAPI.Models.DomainModel;
using PaymentSystemAPI.Models.DTOs;

namespace PaymentSystemAPI.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IBaseRespository<Merchant> _merchantRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MerchantService> _logger;

        public MerchantService(IBaseRespository<Merchant> merchantRepository, IMapper mapper, ILogger<MerchantService> logger)
        {
            _merchantRepository = merchantRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MerchantDTO> GetMerchantByIdAsync(int merchantId)
        {
            var merchant = await _merchantRepository.GetByIdAsync(merchantId);

            if (merchant == null)
            {
                _logger.LogWarning($"Merchant with ID {merchantId} not found.");
                return null;
            }

            return _mapper.Map<MerchantDTO>(merchant);
        }

        public async Task<IEnumerable<MerchantDTO>> GetAllMerchantAsync()
        {
            var merchant = await _merchantRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MerchantDTO>>(merchant);
        }

        public async Task<bool> AddMerchantAsync(MerchantDTO merchantDTO)
        {
            var merchant = _mapper.Map<Merchant>(merchantDTO);
            if (merchant == null)
            {
                return false;

            }
            var Merchant = new Merchant
            {
                BusinessId = merchantDTO.BusinessId,
                BusinessName = merchantDTO.BusinessName,
                FirstName = merchantDTO.FirstName,
                SurName = merchantDTO.SurName,
                DateOfEstablisment = merchantDTO.DateOfEstablisment,
                PhoneNumber = merchantDTO.PhoneNumber,
                AverageTransactionVolume = merchantDTO.AverageTransactionVolume

            };
            await _merchantRepository.AddAsync(merchant);

            return true;
        }

        public async Task UpdateMerchantAsync(int merchantId, MerchantDTO merchantDTO)
        {
            var existingMerchant = await _merchantRepository.GetByIdAsync(merchantId);

            if (existingMerchant == null)
            {
                _logger.LogWarning($"Merchant with ID {merchantId} not found.");
                return;
            }

            _mapper.Map(merchantDTO, existingMerchant);
            await _merchantRepository.UpdateAsync(existingMerchant);
        }

        public async Task DeleteMerchantAsync(int merchantId)
        {
            var merchant = await _merchantRepository.GetByIdAsync(merchantId);

            if (merchant == null)
            {
                _logger.LogWarning($"Merchant with ID {merchantId} not found.");
                return;
            }

            await _merchantRepository.DeleteAsync(merchant);
        }
    }
}
