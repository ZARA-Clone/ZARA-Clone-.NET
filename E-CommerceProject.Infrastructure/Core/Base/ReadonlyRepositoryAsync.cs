using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Core.Base
{
    public class ReadonlyRepositoryAsync<T, TKey> : IReadonlyRepositoryAsync<T, TKey>
        where T : class
    {
        private DbContext _dbContext;
        protected DbContext DbContext => _dbContext;
        public ReadonlyRepositoryAsync(DbContext dbContext)
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
