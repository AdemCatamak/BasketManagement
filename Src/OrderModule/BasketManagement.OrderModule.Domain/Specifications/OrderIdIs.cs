using BasketManagement.OrderModule.Domain.ValueObjects;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.OrderModule.Domain.Specifications
{
    public class OrderIdIs : ExpressionSpecification<Order>
    {
        public OrderIdIs(OrderId orderId) : base(o => Equals(o.Id, orderId))
        {
        }
    }
}