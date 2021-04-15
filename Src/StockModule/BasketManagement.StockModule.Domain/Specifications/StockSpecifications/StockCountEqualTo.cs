using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.StockModule.Domain.Specifications.StockSpecifications
{
    public class StockCountEqualTo : ExpressionSpecification<Stock>
    {
        public StockCountEqualTo(int stockCount) : base(s => s.AvailableStock == stockCount)
        {
        }
    }
}