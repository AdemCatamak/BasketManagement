using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.StockModule.Domain.Exceptions
{
    public class StockAlreadyExistException : ConflictException
    {
        public StockAlreadyExistException(string productId) : base($"Stock already initialized for productId:{productId}")
        {
        }
    }
}