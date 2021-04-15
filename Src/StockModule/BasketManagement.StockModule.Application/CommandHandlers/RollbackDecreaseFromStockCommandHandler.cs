using System.Threading;
using System.Threading.Tasks;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.StockModule.Application.Commands;
using BasketManagement.StockModule.Domain;
using BasketManagement.StockModule.Domain.Repositories;

namespace BasketManagement.StockModule.Application.CommandHandlers
{
    public class RollbackDecreaseFromStockCommandHandler : IDomainCommandHandler<RollbackDecreaseFromStockCommand>
    {
        private readonly IStockDbContext _stockDbContext;
        private readonly IDomainMessageBroker _domainMessageBroker;

        public RollbackDecreaseFromStockCommandHandler(IStockDbContext stockDbContext, IDomainMessageBroker domainMessageBroker)
        {
            _stockDbContext = stockDbContext;
            _domainMessageBroker = domainMessageBroker;
        }

        public async Task<bool> Handle(RollbackDecreaseFromStockCommand request, CancellationToken cancellationToken)
        {
            IStockActionRepository stockActionRepository = _stockDbContext.StockActionRepository;
            StockAction stockAction = await stockActionRepository.GetByCorrelationIdAsync(request.CorrelationId, cancellationToken);

            AddToStockCommand addToStockCommand = new AddToStockCommand(stockAction.ProductId, stockAction.Count, $"{stockAction.CorrelationId}-rollback");
            await _domainMessageBroker.SendAsync(addToStockCommand, cancellationToken);

            return true;
        }
    }
}