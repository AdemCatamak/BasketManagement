using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BasketManagement.OrderModule.Contracts.IntegrationCommands;
using BasketManagement.OrderModule.Domain;
using BasketManagement.OrderModule.Domain.Events;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Domain.Outbox;

namespace BasketManagement.OrderModule.Application.DomainEventHandlers
{
    public class OrderNotFulfilledEvent_SendOrderAllocationRollback : IDomainEventHandler<OrderNotFulfilledEvent>
    {
        private readonly IOutboxClient _outboxClient;

        public OrderNotFulfilledEvent_SendOrderAllocationRollback(IOutboxClient outboxClient)
        {
            _outboxClient = outboxClient;
        }

        public async Task Handle(OrderNotFulfilledEvent notification, CancellationToken cancellationToken)
        {
            Order order = notification.Order;

            OrderRollbackIntegrationCommand orderRollbackIntegrationCommand = new OrderRollbackIntegrationCommand(order.Id, order.OrderLines.Select(x => x.OrderItem).ToList());
            await _outboxClient.AddAsync(orderRollbackIntegrationCommand, cancellationToken);
        }
    }
}