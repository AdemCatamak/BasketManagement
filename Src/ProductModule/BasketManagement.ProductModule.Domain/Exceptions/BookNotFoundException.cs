using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.ProductModule.Domain.Exceptions
{
    public class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException() : base("Book not found")
        {
        }
    }
}