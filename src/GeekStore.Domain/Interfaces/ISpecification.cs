using System.Linq.Expressions;
using GeekStore.Domain.Entities;

namespace GeekStore.Domain.Interfaces
{
    public interface ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T, bool>>? Criteria { get; }
        List<Expression<Func<T, object>>> IncludeExpressions { get; }
        Expression<Func<T, object>>? OrderByExpression { get; }
        Expression<Func<T, object>>? OrderByDescendingExpression { get; }
        bool IsSplitQuery { get; }
    }
}