using E_CommerceProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Core.Base
{
    public class ReadonlyRepositoryAsync<T, TKey> : IReadonlyRepositoryAsync<T, TKey>
        where T : class
    {
        private ECommerceContext _dbContext;
        protected ECommerceContext DbContext => _dbContext;
        public ReadonlyRepositoryAsync(ECommerceContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public virtual async Task<List<T>> GetAllAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public async virtual Task<T?> GetByIdAsync(TKey key)
        {
            return await DbContext.Set<T>().FindAsync(key);
        }
    }
}
