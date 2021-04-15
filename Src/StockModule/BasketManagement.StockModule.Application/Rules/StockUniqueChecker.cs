using System.Threading;
using System.Threading.Tasks;
using BasketManagement.StockModule.Domain.Exceptions;
using BasketManagement.StockModule.Domain.Repositories;
using BasketManagement.StockModule.Domain.Rules;
using BasketManagement.StockModule.Domain.Specifications.StockSpecifications;

namespace BasketManagement.StockModule.Application.Rules
{
    public class StockUniqueChecker : IStockUniqueChecker
    {
        private readonly IStockDbContext _stockDbContext;

        public StockUniqueChecker(IStockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }

        public async Task<bool> CheckAsync(string productId, CancellationToken cancellationToken)
        {
            var stockRepository = _stockDbContext.StockRepository;
            var specification = new ProductIdIs(productId);

            bool isUnique = false;
            try
            {
                await stockRepository.GetFirstAsync(specification, cancellationToken);
            }
            catch (StockNotFoundException)
            {
                isUnique = true;
            }

            return isUnique;
        }
    }
}