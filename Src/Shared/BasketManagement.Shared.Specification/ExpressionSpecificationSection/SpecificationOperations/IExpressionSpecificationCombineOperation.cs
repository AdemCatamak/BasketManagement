using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.Shared.Specification.ExpressionSpecificationSection.SpecificationOperations
{
    public interface IExpressionSpecificationCombineOperator
    {
        IExpressionSpecification<TModel> Combine<TModel>(IExpressionSpecification<TModel> left, IExpressionSpecification<TModel> right);
    }
}