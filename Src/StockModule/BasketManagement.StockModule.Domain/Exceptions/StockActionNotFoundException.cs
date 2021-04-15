using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.StockModule.Domain.Exceptions
{
    public class StockActionNotFoundException : NotFoundException
    {
        public StockActionNotFoundException(string correlationId)
            : base($"Stock action not found with correlationId : {correlationId} ")
        {
        }
    }
}