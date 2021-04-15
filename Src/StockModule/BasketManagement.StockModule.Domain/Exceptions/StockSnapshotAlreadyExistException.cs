using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.StockModule.Domain.Exceptions
{
    public class StockSnapshotAlreadyExistException : ConflictException
    {
        public StockSnapshotAlreadyExistException(string productId) : base($"Stock already generated for product : {productId}")
        {
        }
    }
}