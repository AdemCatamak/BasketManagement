using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BasketManagement.Shared.Infrastructure.Db;
using BasketManagement.StockModule.Domain;
using BasketManagement.StockModule.Domain.Exceptions;
using BasketManagement.StockModule.Domain.Repositories;

namespace BasketManagement.StockModule.Infrastructure.Db.Repositories
{
    public class StockActionRepository : IStockActionRepository
    {
        private readonly EfAppDbContext _appDbContext;

        internal StockActionRepository(EfAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(StockAction stockAction, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(stockAction, cancellationToken);
        }

        public async Task<StockAction> GetByCorrelationIdAsync(string correlationId, CancellationToken cancellationToken)
        {
            StockAction? stockAction = await _appDbContext.Set<StockAction>()
                                                          .FirstOrDefaultAsync(action => action.CorrelationId == correlationId, cancellationToken);

            if (stockAction == null)
                throw new StockActionNotFoundException(correlationId);

            return stockAction;
        }
    }
}