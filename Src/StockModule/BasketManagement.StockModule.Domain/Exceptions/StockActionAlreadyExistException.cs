using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.StockModule.Domain.Exceptions
{
    public class StockActionAlreadyExistException : ConflictException
    {
        public StockActionAlreadyExistException(string correlationId, StockActionTypes stockActionType)
            : base($"{stockActionType} is already executed with {correlationId}")
        {
        }
    }
}