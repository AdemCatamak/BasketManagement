using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.ProductModule.Domain.Specifications
{
    public class BookNameContains : ExpressionSpecification<Book>
    {
        public BookNameContains(string partialBookName) : base(book => book.BookName.Contains(partialBookName))
        {
        }
    }
}