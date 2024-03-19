﻿namespace E_CommerceProject.Infrastructure.Core.Base
{
    public interface IRepositoryAsync<T, TKey> : IReadonlyRepositoryAsync<T, TKey>
        where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();

    }
}
