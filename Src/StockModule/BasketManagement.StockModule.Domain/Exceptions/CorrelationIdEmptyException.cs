using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.StockModule.Domain.Exceptions
{
    public class CorrelationIdEmptyException : ValidationException
    {
        public CorrelationIdEmptyException() : base("Correlation id should not be empty")
        {
        }
    }
}