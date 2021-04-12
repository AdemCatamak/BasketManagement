using System.Threading;
using System.Threading.Tasks;

namespace BasketManagement.StockModule.Domain.Rules
{
    public interface IStockUniqueChecker
    {
        Task<bool> CheckAsync(string productId, CancellationToken cancellationToken);
    }
}