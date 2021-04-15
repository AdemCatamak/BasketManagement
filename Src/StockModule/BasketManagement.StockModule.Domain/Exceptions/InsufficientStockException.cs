using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.StockModule.Domain.Exceptions
{
    public class InsufficientStockException : ValidationException
    {
        public InsufficientStockException(int insufficientItemQuantity, int count)
            : base($"There is {count - insufficientItemQuantity} item in stock. You demand {count}")
        {
        }
    }
}