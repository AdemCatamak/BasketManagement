using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BasketManagement.OrderModule.Contracts.IntegrationEvents;
using BasketManagement.OrderModule.Domain.Events;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Domain.Outbox;

namespace BasketManagement.OrderModule.Application.DomainEventHandlers
{
    public class OrderCreatedEvent_PublishIntegrationEvent : IDomainEventHandler<OrderCreatedEvent>
    {
        private readonly IOutboxClient _outboxClient;

        public OrderCreatedEvent_PublishIntegrationEvent(IOutboxClient outboxClient)
        {
            _outboxClient = outboxClient;
        }

        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            var order = notification.Order;
            var orderSubmittedIntegrationEvent = new OrderSubmittedIntegrationEvent(order.Id,
                                                                                    order.OrderLines.Select(line => line.OrderItem).ToList()
                                                                                   );
            await _outboxClient.AddAsync(orderSubmittedIntegrationEvent, cancellationToken);
        }
    }
}