using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.Infrastructure.Persistence.Specifications
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> specification)
        {
            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.IncludeExpressions.Count > 0)
            {
                query = specification.IncludeExpressions.Aggregate(query, (current, includeExpression) => 
                    query.Include(includeExpression));
            }

            if (specification.OrderByExpression is not null)
            {
                query = query.OrderBy(specification.OrderByExpression);
            }

            if (specification.OrderByDescendingExpression is not null)
            {
                query = query.OrderByDescending(specification.OrderByDescendingExpression);
            }

            if (specification.IsPaginationEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            if (specification.IsDistinct)
            {
                query = query.Distinct();
            }

            if (specification.IsSplitQuery)
            {
                query = query.AsSplitQuery();
            }
            
            return query;
        }

        public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query, ISpecification<T, TResult> specification)
        {
            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.IncludeExpressions.Count > 0)
            {
                query = specification.IncludeExpressions.Aggregate(query, (current, includeExpression) =>
                    query.Include(includeExpression));
            }

            if (specification.OrderByExpression is not null)
            {
                query = query.OrderBy(specification.OrderByExpression);
            }

            if (specification.OrderByDescendingExpression is not null)
            {
                query = query.OrderByDescending(specification.OrderByDescendingExpression);
            }

            if (specification.IsPaginationEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            if (specification.IsSplitQuery)
            {
                query = query.AsSplitQuery();
            }

            var selectQuery = query as IQueryable<TResult>;

            if (specification.SelectExpression is not null)
            {
                selectQuery = query.Select(specification.SelectExpression);
            }

            if (specification.IsDistinct)
            {
                selectQuery = selectQuery?.Distinct();
            }

            return selectQuery ?? query.Cast<TResult>();
        }
    }
}