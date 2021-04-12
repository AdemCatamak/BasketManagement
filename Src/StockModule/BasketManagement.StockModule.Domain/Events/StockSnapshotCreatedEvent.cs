using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.StockModule.Domain.Events
{
    public class StockSnapshotCreatedEvent : IDomainEvent
    {
        public StockSnapshot StockSnapshot { get; private set; }

        public StockSnapshotCreatedEvent(StockSnapshot stockSnapshot)
        {
            StockSnapshot = stockSnapshot;
        }
    }
}