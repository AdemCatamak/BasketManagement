using System.Threading;
using System.Threading.Tasks;
using BasketManagement.StockModule.Domain.Exceptions;
using BasketManagement.StockModule.Domain.Repositories;
using BasketManagement.StockModule.Domain.Rules;

namespace BasketManagement.StockModule.Application.Rules
{
    public class StockActionUniqueChecker : IStockActionUniqueChecker
    {
        private readonly IStockDbContext _stockDbContext;

        public StockActionUniqueChecker(IStockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }

        public async Task<bool> CheckAsync(string correlationId, CancellationToken cancellationToken)
        {
            IStockActionRepository stockActionRepository = _stockDbContext.StockActionRepository;
            bool unique = false;
            try
            {
                await stockActionRepository.GetByCorrelationIdAsync(correlationId, cancellationToken);
            }
            catch (StockActionNotFoundException)
            {
                unique = true;
            }

            return unique;
        }
    }
}