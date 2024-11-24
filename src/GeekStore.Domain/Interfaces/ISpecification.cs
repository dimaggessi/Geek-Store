using System.Linq.Expressions;
using GeekStore.Domain.Entities;

namespace GeekStore.Domain.Interfaces
{
    public interface ISpecification<T> where T : BaseEntity
    {
        bool IsSplitQuery { get; }
        bool IsDistinct { get;}
        bool IsPaginationEnabled { get; }
        int Take { get; }
        int Skip { get; }
        Expression<Func<T, bool>>? Criteria { get; }
        List<Expression<Func<T, object>>> IncludeExpressions { get; }
        Expression<Func<T, object>>? OrderByExpression { get; }
        Expression<Func<T, object>>? OrderByDescendingExpression { get; }
    }

    public interface ISpecification<T, TResult> : ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T, TResult>>? SelectExpression { get; }
    }
}