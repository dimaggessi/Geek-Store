using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T>: IGenericRepository<T> where T : BaseEntity
    {
        private readonly GeekStoreDbContext _context;
        public GenericRepository(GeekStoreDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().CountAsync(x => x.Id == id, cancellationToken) > 0;
        }
    }
}