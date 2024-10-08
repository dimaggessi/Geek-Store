using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Infrastructure.Persistence.Specifications;
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

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().FindAsync(id, cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<T?> GetEntityWithSpecification(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).ToListAsync(cancellationToken);
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

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();
            return SpecificationEvaluator<T>.GetQuery(query, specification);
        }
    }
}