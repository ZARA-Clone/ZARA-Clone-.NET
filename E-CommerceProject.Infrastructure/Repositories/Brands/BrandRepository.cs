using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Repositories.Brands
{
    public class BrandRepository : RepositoryAsync<Brand, int>, IBrandRepository
    {
        private readonly ECommerceContext _dbContext;

        public BrandRepository(ECommerceContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public override async Task<List<Brand>> GetAllAsync()
        {
            return await _dbContext.Set<Brand>()
                .Include(c => c.Products)
                .ToListAsync();
        }
        public override async Task<Brand?> GetByIdAsync(int key)
        {
            return await _dbContext.Set<Brand>()
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == key);
        }
    }
}
