using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentSystemAPI.Interfaces.IServices;
using PaymentSystemAPI.Models.DTOs;

namespace PaymentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantService _merchantService;
        private readonly ILogger<MerchantController> _logger;

        public MerchantController(IMerchantService merchantService, ILogger<MerchantController> logger)
        {
            _merchantService = merchantService;
            _logger = logger;
        }

        [HttpGet("{merchantId}")]
        public async Task<IActionResult> GetMerchantById(int merchantId)
        {
            try
            {
                var merchant = await _merchantService.GetMerchantByIdAsync(merchantId);

                if (merchant == null)
                    return NotFound($"Merchant with ID {merchantId} not found.");

                return Ok(merchant);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting merchant with ID {merchantId}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMerchant()
        {
            try
            {
                var merchant = await _merchantService.GetAllMerchantAsync();
                return Ok(merchant);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting all merchant: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMerchant([FromBody] MerchantDTO merchantDTO)
        {
            try
            {
                if (merchantDTO == null)
                    return BadRequest("Invalid merchant data");

                await _merchantService.AddMerchantAsync(merchantDTO);
                return Ok("Merchant added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding merchant: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{merchantId}")]
        public async Task<IActionResult> UpdateMerchant(int merchantId, [FromBody] MerchantDTO merchantDTO)
        {
            try
            {
                await _merchantService.UpdateMerchantAsync(merchantId, merchantDTO);
                return Ok("Merchant updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating merchant with ID {merchantId}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{merchantId}")]
        public async Task<IActionResult> DeleteMerchant(int merchantId)
        {
            try
            {
                await _merchantService.DeleteMerchantAsync(merchantId);
                return Ok("Merchant deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting merchant with ID {merchantId}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
