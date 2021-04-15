using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.StockModule.Domain.Specifications.StockSpecifications
{
    public class ProductIdIs : ExpressionSpecification<Stock>
    {
        public ProductIdIs(string productId) : base(stock => stock.ProductId == productId)
        {
        }
    }
}