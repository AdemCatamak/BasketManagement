using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.BasketModule.Domain.Events
{
    public class BasketLineCreatedEvent : IDomainEvent
    {
        public BasketLine BasketLine { get; private set; }

        public BasketLineCreatedEvent(BasketLine basketLine)
        {
            BasketLine = basketLine;
        }
    }
}