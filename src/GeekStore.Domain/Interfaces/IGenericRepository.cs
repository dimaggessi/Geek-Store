using GeekStore.Domain.Entities;

namespace GeekStore.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}