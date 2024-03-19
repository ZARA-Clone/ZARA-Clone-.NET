namespace E_CommerceProject.Infrastructure.Core.Base
{
    public interface IReadonlyRepositoryAsync<T, TKey>
        where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(TKey key);
    }
}
