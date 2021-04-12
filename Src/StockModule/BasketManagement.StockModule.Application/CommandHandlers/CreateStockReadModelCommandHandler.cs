using System.Threading;
using System.Threading.Tasks;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.StockModule.Application.Commands;
using BasketManagement.StockModule.Domain;
using BasketManagement.StockModule.Domain.Repositories;
using BasketManagement.StockModule.Domain.Rules;

namespace BasketManagement.StockModule.Application.CommandHandlers
{
    public class CreateStockReadModelCommandHandler : IDomainCommandHandler<CreateStockReadModelCommand>
    {
        private readonly IStockDbContext _stockDbContext;
        private readonly IStockUniqueChecker _stockUniqueChecker;

        public CreateStockReadModelCommandHandler(IStockDbContext stockDbContext, IStockUniqueChecker stockUniqueChecker)
        {
            _stockDbContext = stockDbContext;
            _stockUniqueChecker = stockUniqueChecker;
        }

        public async Task<bool> Handle(CreateStockReadModelCommand request, CancellationToken cancellationToken)
        {
            IStockRepository stockRepository = _stockDbContext.StockRepository;

            var stock = Stock.Create(request.ProductId, request.AvailableStockCount, request.LastStockActionId, request.LastStockUpdatedOn, _stockUniqueChecker, cancellationToken);
            await stockRepository.AddAsync(stock, cancellationToken);

            return true;
        }
    }
}