using System.Threading;
using System.Threading.Tasks;

namespace BasketManagement.StockModule.Domain.Rules
{
    public interface IStockActionUniqueChecker
    {
        Task<bool> CheckAsync(string correlationId, CancellationToken cancellationToken);
    }
}