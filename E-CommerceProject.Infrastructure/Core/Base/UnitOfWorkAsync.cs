using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Repositories.Brands;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Core.Base
{
    public class UnitOfWorkAsync : IUnitOfWorkAsync
    {
        private readonly ECommerceContext _dbContext;
        private readonly IBrandRepository _brandRepository;

        //protected DbContext DbContext { get; }

        public IBrandRepository BrandRepository => _brandRepository;

        public UnitOfWorkAsync(ECommerceContext dbContext
            , IBrandRepository brandRepository)
        {
            _dbContext = dbContext;
            _brandRepository = brandRepository;
        }


        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
