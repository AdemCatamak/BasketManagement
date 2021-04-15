using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.StockModule.Domain.Exceptions
{
    public class CountNotPositiveException : ValidationException
    {
        public CountNotPositiveException() : base("Count should be positive number")
        {
        }
    }
}