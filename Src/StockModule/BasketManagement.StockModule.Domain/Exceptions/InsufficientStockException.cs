using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.StockModule.Domain.Exceptions
{
    public class InsufficientStockException : ValidationException
    {
        public InsufficientStockException(int availableStock, int count)
            : base($"There is {availableStock} item in stock. You demand {count}")
        {
        }
    }
}