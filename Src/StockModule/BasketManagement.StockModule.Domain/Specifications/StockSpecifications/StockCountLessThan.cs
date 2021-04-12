using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.StockModule.Domain.Specifications.StockSpecifications
{
    public class StockCountLessThan : ExpressionSpecification<Stock>
    {
        public StockCountLessThan(int stockCount) : base(s => s.AvailableStock < stockCount)
        {
        }
    }
}