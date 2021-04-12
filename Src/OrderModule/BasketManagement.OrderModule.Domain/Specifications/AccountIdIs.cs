using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.OrderModule.Domain.Specifications
{
    public class AccountIdIs : ExpressionSpecification<Order>
    {
        public AccountIdIs(string accountId) : base(o => o.AccountId == accountId)
        {
        }
    }
}