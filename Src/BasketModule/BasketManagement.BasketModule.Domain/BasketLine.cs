using System;
using BasketManagement.BasketModule.Domain.Events;
using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain;

namespace BasketManagement.BasketModule.Domain
{
    public class BasketLine : DomainEventHolder
    {
        public BasketLineId Id { get; private set; } = null!;
        public BasketId BasketId { get; set; }
        public virtual Basket Basket { get; private set; } = null!;
        public BasketItem BasketItem { get; private set; } = null!;

        private BasketLine()
        {
            // Only for EF
        }

        private BasketLine(Basket basket, BasketItem basketItem)
            : this(new BasketLineId(Guid.NewGuid()), basket, basketItem)
        {
        }

        private BasketLine(BasketLineId id, Basket basket, BasketItem basketItem)
        {
            Id = id;
            BasketId = basket.Id;
            Basket = basket;
            BasketItem = basketItem;
        }

        public static BasketLine Create(Basket basket, BasketItem basketItem)
        {
            BasketLine basketLine = new BasketLine(basket, basketItem);
            BasketLineCreatedEvent basketLineCreatedEvent = new BasketLineCreatedEvent(basketLine);
            basketLine.AddDomainEvent(basketLineCreatedEvent);

            return basketLine;
        }

        public void UpdateQuantity(int quantity)
        {
            int oldQuantity = BasketItem.Quantity;
            BasketItem = new BasketItem(BasketItem.ProductId, quantity);
            if (oldQuantity > quantity)
            {
                BasketLineQuantityDecreasedEvent basketLineQuantityDecreasedEvent = new BasketLineQuantityDecreasedEvent(oldQuantity, this);
                AddDomainEvent(basketLineQuantityDecreasedEvent);
            }

            if (BasketItem.Quantity < quantity)
            {
                BasketLineQuantityIncreasedEvent basketLineQuantityIncreasedEvent = new BasketLineQuantityIncreasedEvent(oldQuantity, this);
                AddDomainEvent(basketLineQuantityIncreasedEvent);
            }
        }
    }
}