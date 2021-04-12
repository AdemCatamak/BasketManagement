using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.BasketModule.Domain.Specifications
{
    public class AccountIdIs : ExpressionSpecification<Basket>
    {
        public AccountIdIs(string accountId) : base(o => o.AccountId == accountId)
        {
        }
    }
}