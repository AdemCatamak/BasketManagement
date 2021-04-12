using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.ProductModule.Domain.Specifications
{
    public class AuthorNameContains : ExpressionSpecification<Book>
    {
        public AuthorNameContains(string partialAuthorName) : base(book => book.AuthorName.Contains(partialAuthorName))
        {
        }
    }
}