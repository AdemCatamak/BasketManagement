using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.BasketModule.Domain.Events
{
    public class BasketCreatedEvent : IDomainEvent
    {
        public Basket Basket { get; private set; }

        public BasketCreatedEvent(Basket basket)
        {
            Basket = basket;
        }
    }
}