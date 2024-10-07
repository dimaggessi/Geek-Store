using GeekStore.Domain.Entities;

namespace GeekStore.Domain.Interfaces
{
    public interface IUnityOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : BaseEntity;
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);
    }
}