using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.BasketModule.Domain.Events
{
    public abstract class ABasketLineQuantityChangedEvent : IDomainEvent
    {
        public int OldQuantity { get; private set; }
        public BasketLine BasketLine { get; private set; }

        protected ABasketLineQuantityChangedEvent(int oldQuantity, BasketLine basketLine)
        {
            OldQuantity = oldQuantity;
            BasketLine = basketLine;
        }
    }

    public class BasketLineQuantityIncreasedEvent : ABasketLineQuantityChangedEvent
    {
        public BasketLineQuantityIncreasedEvent(int oldQuantity, BasketLine basketLine)
            : base(oldQuantity, basketLine)
        {
        }
    }

    public class BasketLineQuantityDecreasedEvent : ABasketLineQuantityChangedEvent
    {
        public BasketLineQuantityDecreasedEvent(int oldQuantity, BasketLine basketLine)
            : base(oldQuantity, basketLine)
        {
        }
    }
}