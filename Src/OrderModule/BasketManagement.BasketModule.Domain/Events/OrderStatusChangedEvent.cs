using System;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.BasketModule.Domain.Events
{
    public abstract class OrderStatusChangedEvent : IDomainEvent
    {
        public BasketStatuses? PreviousOrderStatus { get; private set; }
        public Basket Basket { get; private set; }

        protected OrderStatusChangedEvent(BasketStatuses? previousOrderStatus, Basket basket)
        {
            PreviousOrderStatus = previousOrderStatus;
            Basket = basket;
        }

        public static OrderStatusChangedEvent Create(BasketStatuses previousBasketStatus, Basket basket)
        {
            OrderStatusChangedEvent orderStatusChangedEvent
                = basket.BasketStatus switch
                  {
                      BasketStatuses.OrderNotFulfilled => new OrderNotFulfilledEvent(previousBasketStatus, basket),
                      BasketStatuses.OrderFulfilled => new OrderFulfilledEvent(previousBasketStatus, basket),
                      BasketStatuses.Shipped => new OrderShippedEvent(previousBasketStatus, basket),
                      _ => throw new ArgumentOutOfRangeException()
                  };

            return orderStatusChangedEvent;
        }
    }

    public class OrderFulfilledEvent : OrderStatusChangedEvent
    {
        internal OrderFulfilledEvent(BasketStatuses? previousOrderStatus, Basket basket) : base(previousOrderStatus, basket)
        {
        }
    }

    public class OrderNotFulfilledEvent : OrderStatusChangedEvent
    {
        internal OrderNotFulfilledEvent(BasketStatuses? previousOrderStatus, Basket basket) : base(previousOrderStatus, basket)
        {
        }
    }

    public class OrderShippedEvent : OrderStatusChangedEvent
    {
        internal OrderShippedEvent(BasketStatuses? previousOrderStatus, Basket basket) : base(previousOrderStatus, basket)
        {
        }
    }
}