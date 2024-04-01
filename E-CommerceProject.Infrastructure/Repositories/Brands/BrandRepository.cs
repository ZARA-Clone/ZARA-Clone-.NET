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

        public async Task<(List<Brand> items, int totalItemsCount)> Get(string? name
            , int? categoryId, int pageIndex = 0, int pageSize = 10)
        {
            if (pageIndex < 0)
                throw new ArgumentOutOfRangeException("pageIndex", "Page index can't be less than zero");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", "Page size can't be less than zero");

            var query = _dbContext.Set<Brand>()
                .Include(c => c.Category)
                .Include(c => c.Products)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name));
            if (categoryId > 0)
                query = query.Where(x => x.CategoryId == categoryId);

            var totalItems = await query.CountAsync();
            var items = await query.OrderBy(c => c.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalItems);
        }

        public override async Task<List<Brand>> GetAllAsync()
        {
            return await _dbContext.Set<Brand>()
                .Include(c => c.Category)
                .Include(c => c.Products)
                .OrderBy(c => c.Id)
                .ToListAsync();
        }
        public override async Task<Brand?> GetByIdAsync(int key)
        {
            return await _dbContext.Set<Brand>()
                .Include(c => c.Category)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == key);
        }

        public Task<bool> IsNameExist(Brand entity)
        {
            return _dbContext.Set<Brand>()
                .AnyAsync(c => c.Id != entity.Id
                        && c.Name.Trim() == entity.Name.Trim());
        }
    }
}
