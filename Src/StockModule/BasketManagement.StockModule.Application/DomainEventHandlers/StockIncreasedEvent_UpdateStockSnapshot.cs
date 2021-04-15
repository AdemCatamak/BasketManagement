using System.Threading;
using System.Threading.Tasks;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.StockModule.Domain;
using BasketManagement.StockModule.Domain.Events;
using BasketManagement.StockModule.Domain.Repositories;

namespace BasketManagement.StockModule.Application.DomainEventHandlers
{
    public class StockIncreasedEvent_UpdateStockSnapshot : IDomainEventHandler<StockIncreasedEvent>
    {
        private readonly IStockDbContext _stockDbContext;

        public StockIncreasedEvent_UpdateStockSnapshot(IStockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }

        public async Task Handle(StockIncreasedEvent notification, CancellationToken cancellationToken)
        {
            StockAction stockAction = notification.StockAction;

            IStockSnapshotRepository stockSnapshotRepository = _stockDbContext.StockSnapshotRepository;
            StockSnapshot stockSnapshot = await stockSnapshotRepository.GetByProductIdAsync(stockAction.ProductId, cancellationToken);

            stockSnapshot.IncreaseStock(stockAction.Count, stockAction.Id, stockAction.CreatedOn);
        }
    }
}