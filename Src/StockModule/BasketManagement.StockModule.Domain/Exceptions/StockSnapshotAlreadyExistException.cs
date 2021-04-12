using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.StockModule.Domain.Exceptions
{
    public class StockSnapshotAlreadyExistException : ConflictException
    {
        public StockSnapshotAlreadyExistException(string productId) : base($"Stock snapshot already exist for product : {productId}")
        {
        }
    }
}