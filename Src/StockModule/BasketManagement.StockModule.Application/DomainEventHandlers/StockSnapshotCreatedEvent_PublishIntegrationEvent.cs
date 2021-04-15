using System.Threading;
using System.Threading.Tasks;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Domain.Outbox;
using BasketManagement.StockModule.Contracts;
using BasketManagement.StockModule.Domain;
using BasketManagement.StockModule.Domain.Events;

namespace BasketManagement.StockModule.Application.DomainEventHandlers
{
    public class StockSnapshotCreatedEvent_PublishIntegrationEvent : IDomainEventHandler<StockSnapshotCreatedEvent>
    {
        private readonly IOutboxClient _outboxClient;

        public StockSnapshotCreatedEvent_PublishIntegrationEvent(IOutboxClient outboxClient)
        {
            _outboxClient = outboxClient;
        }

        public async Task Handle(StockSnapshotCreatedEvent notification, CancellationToken cancellationToken)
        {
            StockSnapshot stockSnapshot = notification.StockSnapshot;
            var stockSnapshotCreatedIntegrationEvent = new StockSnapshotCreatedIntegrationEvent(stockSnapshot.Id,
                                                                                                stockSnapshot.ProductId,
                                                                                                stockSnapshot.CreatedOn);
            await _outboxClient.AddAsync(stockSnapshotCreatedIntegrationEvent, cancellationToken);
        }
    }
}