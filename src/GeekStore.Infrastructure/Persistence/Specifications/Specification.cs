using System.Linq.Expressions;
using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;

namespace GeekStore.Infrastructure.Persistence.Specifications
{
    public abstract class Specification<T> : ISpecification<T> where T : BaseEntity
    {
        protected Specification(Expression<Func<T, bool>>? criteria) =>
            Criteria = criteria;

        public bool IsSplitQuery { get; protected set; }

        // specify a match for where statement
        public Expression<Func<T, bool>>? Criteria {get;}
        // specify include statements
        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = [];
        // specify orderby statement
        public Expression<Func<T, object>>? OrderByExpression { get; private set; }
        // specify orderbydescending statement
        public Expression<Func<T, object>>? OrderByDescendingExpression { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression) => 
            IncludeExpressions.Add(includeExpression);

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) =>
            OrderByExpression = orderByExpression;

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression) =>
            OrderByDescendingExpression = orderByDescendingExpression;

    }
}