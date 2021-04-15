using System.Threading;
using System.Threading.Tasks;
using BasketManagement.StockModule.Domain.Exceptions;
using BasketManagement.StockModule.Domain.Repositories;
using BasketManagement.StockModule.Domain.Rules;

namespace BasketManagement.StockModule.Application.Rules
{
    public class StockSnapshotUniqueChecker : IStockSnapshotUniqueChecker
    {
        private readonly IStockDbContext _stockDbContext;

        public StockSnapshotUniqueChecker(IStockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }

        public async Task<bool> CheckAsync(string productId, CancellationToken cancellationToken)
        {
            var snapshotRepository = _stockDbContext.StockSnapshotRepository;

            bool unique = false;
            try
            {
                await snapshotRepository.GetByProductIdAsync(productId, cancellationToken);
            }
            catch (StockSnapshotNotFoundException)
            {
                unique = true;
            }

            return unique;
        }
    }
}