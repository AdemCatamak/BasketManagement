using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.ProductModule.Domain.Exceptions
{
    public class BookNameEmptyException : ValidationException
    {
        public BookNameEmptyException() : base("Book name should not be empty")
        {
        }
    }
}