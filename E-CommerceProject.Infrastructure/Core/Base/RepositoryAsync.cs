using E_CommerceProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Core.Base
{
    public class RepositoryAsync<T, TKey> : ReadonlyRepositoryAsync<T, TKey>, IRepositoryAsync<T, TKey>
        where T : class
    {
        public RepositoryAsync(ECommerceContext dbContext) : base(dbContext)
        {
        }

        public virtual Task AddAsync(T entity)
        {
            DbContext.Set<T>()
                .Add(entity);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(T entity)
        {
            DbContext.Set<T>()
                .Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task SaveAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(T entity)
        {
            var entry = DbContext.Entry(entity);
            if (entry.State != EntityState.Detached)
                DbContext.Set<T>()
                    .Attach(entity);

            entry.State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
