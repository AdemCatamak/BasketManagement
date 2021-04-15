using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.StockModule.Domain.Exceptions
{
    public class StockNotFoundException : NotFoundException
    {
        public StockNotFoundException() : base("Stock not found")
        {
        }
    }
}