using System;
using System.Linq.Expressions;

namespace BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications
{
    public class DynamicExpressionSpecification<T> : ExpressionSpecification<T>
    {
        public DynamicExpressionSpecification(Expression<Func<T, bool>> expression) : base(expression)
        {
        }
    }
}