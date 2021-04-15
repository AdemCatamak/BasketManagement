using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.StockModule.Domain.Specifications.StockSpecifications
{
    public class StockCountGreaterThan : ExpressionSpecification<Stock>
    {
        public StockCountGreaterThan(int stockCount) : base(s => s.AvailableStock > stockCount)
        {
        }
    }
}