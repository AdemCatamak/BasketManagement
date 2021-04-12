using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.BasketModule.Domain.Specifications
{
    public class BasketIdIs : ExpressionSpecification<Basket>
    {
        public BasketIdIs(BasketId basketId) : base(o => Equals(o.Id, basketId))
        {
        }
    }
}