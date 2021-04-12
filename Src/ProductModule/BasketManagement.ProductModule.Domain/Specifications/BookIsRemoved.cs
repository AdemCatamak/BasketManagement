using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.ProductModule.Domain.Specifications
{
    public class BookIsRemoved : ExpressionSpecification<Book>
    {
        public BookIsRemoved() : base(book => book.IsDeleted)
        {
        }
    }
}