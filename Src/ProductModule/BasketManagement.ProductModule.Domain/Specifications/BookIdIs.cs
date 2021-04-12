using BasketManagement.ProductModule.Domain.ValueObjects;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.ProductModule.Domain.Specifications
{
    public class BookIdIs : ExpressionSpecification<Book>
    {
        public BookIdIs(BookId bookId) : base(book => Equals(book.Id, bookId))
        {
        }
    }
}