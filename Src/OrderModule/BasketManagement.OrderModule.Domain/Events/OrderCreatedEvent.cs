using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.OrderModule.Domain.Events
{
    public class OrderCreatedEvent : IDomainEvent
    {
        public Order Order { get; private set; }

        public OrderCreatedEvent(Order order)
        {
            Order = order;
        }
    }
}