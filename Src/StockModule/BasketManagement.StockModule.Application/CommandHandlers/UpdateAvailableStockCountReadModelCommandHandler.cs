using System.Threading;
using System.Threading.Tasks;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;
using BasketManagement.StockModule.Application.Commands;
using BasketManagement.StockModule.Domain;
using BasketManagement.StockModule.Domain.Repositories;
using BasketManagement.StockModule.Domain.Specifications.StockSpecifications;

namespace BasketManagement.StockModule.Application.CommandHandlers
{
    public class UpdateAvailableStockCountReadModelCommandHandler : IDomainCommandHandler<UpdateAvailableStockCountReadModelCommand>
    {
        private readonly IStockDbContext _stockDbContext;

        public UpdateAvailableStockCountReadModelCommandHandler(IStockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }

        public async Task<bool> Handle(UpdateAvailableStockCountReadModelCommand request, CancellationToken cancellationToken)
        {
            IExpressionSpecification<Stock> specification = new ProductIdIs(request.ProductId);
            var stockRepository = _stockDbContext.StockRepository;
            Stock stock = await stockRepository.GetFirstAsync(specification, cancellationToken);

            if (stock.LastStockActionDate > request.LastStockUpdatedOn)
            {
                return false;
            }

            stock.UpdateStock(request.AvailableStockCount, request.LastStockActionId, request.LastStockUpdatedOn);
            return true;
        }
    }
}