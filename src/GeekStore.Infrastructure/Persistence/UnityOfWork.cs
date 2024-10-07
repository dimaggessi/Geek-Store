using System.Collections.Concurrent;
using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Infrastructure.Persistence.Repositories;

namespace GeekStore.Infrastructure.Persistence
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly GeekStoreDbContext _context;
        private readonly ConcurrentDictionary<string, object> _repositories = new();
        public UnityOfWork(GeekStoreDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public IGenericRepository<T> GetRepository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;

            return (IGenericRepository<T>)_repositories.GetOrAdd(type, t => 
            {
                var repositoryType = typeof(GenericRepository<>).MakeGenericType(typeof(T));

                return Activator.CreateInstance(repositoryType, _context) 
                    ?? throw new InvalidOperationException($"Could not create repository instance for {t}");
            });
        }

        public void Dispose() => _context.Dispose();
    }
}