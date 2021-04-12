using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.ProductModule.Domain.Exceptions
{
    public class AuthorNameEmptyException : ValidationException
    {
        public AuthorNameEmptyException() : base("Author name should not be empty")
        {
        }
    }
}