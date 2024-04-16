using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Repositories.Products
{
    public class ProductsRepository : RepositoryAsync<Product, int>, IProductsRepository
    {
        private readonly ECommerceContext _dbContext;

        public ProductsRepository(ECommerceContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<Product?> GetByIdAsync(int key)
        {
            return await _dbContext.Set<Product>()
                        .Include(c => c.Brand)
                        .Include(c => c.ProductImages)
                        .Include(c => c.ProductSizes)
                        .OrderBy(c => c.Id)
                        .FirstOrDefaultAsync(p => p.Id == key);

        }
        public override async Task<List<Product>> GetAllAsync()
        {
            return await _dbContext.Set<Product>()
                        .Include(c => c.Brand)
                        .Include(c => c.ProductImages)
                        .Include(c => c.ProductSizes)
                        .OrderBy(c => c.Id)
                        .ToListAsync();
        }

        public async Task<(List<Product> items, int totalItemsCount)> Get(string? name
            , int? brandId, decimal? minPrice, decimal? maxPrice
            , int pageIndex = 0, int pageSize = 10)
        {
            if (pageIndex < 0)
                throw new ArgumentOutOfRangeException("pageIndex", "Page index can't be less than zero");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", "Page size can't be less than zero");

            var query = _dbContext.Set<Product>()
                .Include(c => c.Brand)
                .Include(c => c.ProductImages)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name));
            if (brandId > 0)
                query = query.Where(x => x.BrandId == brandId);
            if (minPrice > 0)
                query = query.Where(x => x.Price - ((x.Price * x.Discount) / 100) >= minPrice);
            if (maxPrice > 0)
                query = query.Where(x => x.Price - ((x.Price * x.Discount) / 100) <= maxPrice);

            var totalItems = await query.CountAsync();
            var items = await query.OrderBy(c => c.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalItems);

        }
        public Task<bool> IsNameExist(Product entity)
        {
            return _dbContext.Set<Product>()
                .AnyAsync(c => c.Id != entity.Id
                        && c.BrandId != entity.BrandId
                        && c.Name.Trim() == entity.Name.Trim());
        }
    }
}
