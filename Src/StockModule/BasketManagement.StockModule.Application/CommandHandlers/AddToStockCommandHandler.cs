using System.Threading;
using System.Threading.Tasks;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.StockModule.Application.Commands;
using BasketManagement.StockModule.Domain;
using BasketManagement.StockModule.Domain.Repositories;
using BasketManagement.StockModule.Domain.Rules;

namespace BasketManagement.StockModule.Application.CommandHandlers
{
    public class AddToStockCommandHandler : IDomainCommandHandler<AddToStockCommand>
    {
        private readonly IStockActionUniqueChecker _stockActionUniqueChecker;
        private readonly IStockDbContext _stockDbContext;

        public AddToStockCommandHandler(IStockActionUniqueChecker stockActionUniqueChecker, IStockDbContext stockDbContext)
        {
            _stockActionUniqueChecker = stockActionUniqueChecker;
            _stockDbContext = stockDbContext;
        }

        public async Task<bool> Handle(AddToStockCommand request, CancellationToken cancellationToken)
        {
            var stockActionModel = StockAction.Create(request.ProductId, StockActionTypes.AddToStock, request.Count, request.CorrelationId, _stockActionUniqueChecker, cancellationToken);

            IStockActionRepository stockActionRepository = _stockDbContext.StockActionRepository;
            await stockActionRepository.AddAsync(stockActionModel, cancellationToken);

            return true;
        }
    }
}