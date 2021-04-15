using System.Threading;
using System.Threading.Tasks;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Domain.Outbox;
using BasketManagement.StockModule.Contracts;
using BasketManagement.StockModule.Domain;
using BasketManagement.StockModule.Domain.Events;

namespace BasketManagement.StockModule.Application.DomainEventHandlers
{
    public class AvailableStockCountChangedEvent_PublishIntegrationEvent : IDomainEventHandler<AvailableStockCountChangedEvent>
    {
        private readonly IOutboxClient _outboxClient;

        public AvailableStockCountChangedEvent_PublishIntegrationEvent(IOutboxClient outboxClient)
        {
            _outboxClient = outboxClient;
        }

        public async Task Handle(AvailableStockCountChangedEvent notification, CancellationToken cancellationToken)
        {
            StockSnapshot stockSnapshot = notification.StockSnapshot;

            var availableStockCountChangedIntegrationEvent = new AvailableStockCountChangedIntegrationEvent(stockSnapshot.ProductId,
                                                                                                            stockSnapshot.AvailableStock,
                                                                                                            stockSnapshot.LastStockActionDate,
                                                                                                            stockSnapshot.StockActionId);

            await _outboxClient.AddAsync(availableStockCountChangedIntegrationEvent, cancellationToken);
        }
    }
}