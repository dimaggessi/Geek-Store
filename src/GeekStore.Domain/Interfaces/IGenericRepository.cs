using GeekStore.Domain.Entities;

namespace GeekStore.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T?> GetEntityWithSpecification(ISpecification<T> specification,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> specification,
            CancellationToken cancellationToken = default);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
    }
}