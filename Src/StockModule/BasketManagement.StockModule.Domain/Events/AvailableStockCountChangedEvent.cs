using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.StockModule.Domain.Events
{
    public class AvailableStockCountChangedEvent : IDomainEvent
    {
        public StockSnapshot StockSnapshot { get; private set; }

        public AvailableStockCountChangedEvent(StockSnapshot stockSnapshot)
        {
            StockSnapshot = stockSnapshot;
        }
    }
}