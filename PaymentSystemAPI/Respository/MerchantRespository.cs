using Microsoft.EntityFrameworkCore;
using PaymentSystemAPI.Data;
using PaymentSystemAPI.Interfaces.IRespository;
using PaymentSystemAPI.Models.DomainModel;

namespace PaymentSystemAPI.Respository
{
    public class MerchantRepository : IBaseRespository<Merchant>
    {
        private readonly AppDbContext _dbContext;

        public MerchantRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Merchant> GetByIdAsync(int id)
        {
            return await _dbContext.Merchants.FindAsync(id);
        }

        public async Task<IEnumerable<Merchant>> GetAllAsync()
        {
            return await _dbContext.Merchants.ToListAsync();
        }

        public async Task AddAsync(Merchant merchant)
        {
            _dbContext.Merchants.Add(merchant);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(Merchant merchant)
        {
            _dbContext.Entry(merchant).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Merchant merchant)
        {
            _dbContext.Merchants.Remove(merchant);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
