using System;
using BasketManagement.BasketModule.Domain.Events;
using BasketManagement.BasketModule.Domain.Exceptions;
using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain;

namespace BasketManagement.BasketModule.Domain
{
    public class BasketLine : DomainEventHolder
    {
        public BasketLineId Id { get; private set; } = null!;
        public BasketId BasketId { get; set; } = null!;
        public virtual Basket Basket { get; private set; } = null!;
        public BasketItem BasketItem { get; private set; } = null!;
        public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; private set; } = DateTime.UtcNow;
        public bool IsDeleted { get; private set; } = false;
        public byte[] RowVersion { get; private set; }

        private BasketLine()
        {
            // Only for EF
        }

        private BasketLine(Basket basket, BasketItem basketItem)
            : this(new BasketLineId(Guid.NewGuid()), basket, basketItem, DateTime.UtcNow, DateTime.UtcNow, false)
        {
        }

        private BasketLine(BasketLineId id, Basket basket, BasketItem basketItem, DateTime updatedOn, DateTime createdOn, bool isDeleted)
        {
            Id = id;
            BasketId = basket.Id;
            Basket = basket;
            BasketItem = basketItem;
            UpdatedOn = updatedOn;
            CreatedOn = createdOn;
            IsDeleted = isDeleted;
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
            if (quantity < 0)
            {
                throw new NegativeQuantityException();
            }

            UpdatedOn = DateTime.UtcNow;
            int oldQuantity = BasketItem.Quantity;
            BasketItem = new BasketItem(BasketItem.ProductId, quantity);
            if (oldQuantity > quantity)
            {
                BasketLineQuantityDecreasedEvent basketLineQuantityDecreasedEvent = new BasketLineQuantityDecreasedEvent(oldQuantity, this);
                AddDomainEvent(basketLineQuantityDecreasedEvent);
            }

            if (oldQuantity < quantity)
            {
                BasketLineQuantityIncreasedEvent basketLineQuantityIncreasedEvent = new BasketLineQuantityIncreasedEvent(oldQuantity, this);
                AddDomainEvent(basketLineQuantityIncreasedEvent);
            }

            if (quantity == 0)
            {
                SetRemoved();
            }
            
            UpdatedOn = DateTime.UtcNow;
        }

        public void Remove()
        {
            UpdateQuantity(0);
        }

        private void SetRemoved()
        {
            IsDeleted = true;
            UpdatedOn = DateTime.UtcNow;
        }
    }
}