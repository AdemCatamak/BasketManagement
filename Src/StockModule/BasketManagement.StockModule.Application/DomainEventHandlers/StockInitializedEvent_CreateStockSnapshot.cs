using System.Threading;
using System.Threading.Tasks;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.StockModule.Domain;
using BasketManagement.StockModule.Domain.Events;
using BasketManagement.StockModule.Domain.Repositories;
using BasketManagement.StockModule.Domain.Rules;

namespace BasketManagement.StockModule.Application.DomainEventHandlers
{
    public class StockInitializedEvent_CreateStockSnapshot : IDomainEventHandler<StockInitializedEvent>
    {
        private readonly IStockSnapshotUniqueChecker _snapshotUniqueChecker;
        private readonly IStockDbContext _stockDbContext;

        public StockInitializedEvent_CreateStockSnapshot(IStockSnapshotUniqueChecker snapshotUniqueChecker, IStockDbContext stockDbContext)
        {
            _snapshotUniqueChecker = snapshotUniqueChecker;
            _stockDbContext = stockDbContext;
        }

        public async Task Handle(StockInitializedEvent notification, CancellationToken cancellationToken)
        {
            StockAction stockAction = notification.StockAction;
            StockSnapshot stockSnapshot = StockSnapshot.Create(stockAction.ProductId, stockAction.Count, stockAction.Id, stockAction.CreatedOn, _snapshotUniqueChecker, cancellationToken);

            IStockSnapshotRepository snapshotRepository = _stockDbContext.StockSnapshotRepository;
            await snapshotRepository.AddAsync(stockSnapshot, cancellationToken);
        }
    }
}