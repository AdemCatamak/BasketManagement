using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.Shared.Specification.ExpressionSpecificationSection.SpecificationOperations
{
    public interface IExpressionSpecificationOperator
    {
        IExpressionSpecification<TModel> Apply<TModel>(IExpressionSpecification<TModel> specification);
    }
}