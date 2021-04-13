using System;
using System.Collections.Generic;
using System.Linq;
using BasketManagement.BasketModule.Domain.Events;
using BasketManagement.BasketModule.Domain.Exceptions;
using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain;

namespace BasketManagement.BasketModule.Domain
{
    public class Basket : DomainEventHolder
    {
        public BasketId Id { get; private set; }
        public string AccountId { get; private set; }
        public DateTime UpdatedOn { get; private set; }
        public DateTime CreatedOn { get; private set; }

        private readonly List<BasketLine> _basketLines = new List<BasketLine>();
        public IReadOnlyCollection<BasketLine> BasketLines => _basketLines;


        private Basket(string accountId) : this
            (new BasketId(Guid.NewGuid()), accountId, DateTime.UtcNow, DateTime.UtcNow)
        {
        }

        private Basket(BasketId id, string accountId,  DateTime createdOn, DateTime updatedOn)
        {
            Id = id;
            AccountId = accountId;
            UpdatedOn = updatedOn;
            CreatedOn = createdOn;
        }

        public static Basket Create(string accountId)
        {
            Basket basket = new Basket(accountId);
            BasketCreatedEvent basketCreatedEvent = new BasketCreatedEvent(basket);
            basket.AddDomainEvent(basketCreatedEvent);

            return basket;
        }

        public void PutItemIntoBasket(BasketItem basketItem)
        {
            var existBasketLine = _basketLines.FirstOrDefault(line => line.BasketItem.ProductId == basketItem.ProductId);
            if (existBasketLine != null)
            {
                if (basketItem.Quantity != 0)
                    existBasketLine.UpdateQuantity(basketItem.Quantity);
                else
                    RemoveBasketLine(existBasketLine);
            }
            else
            {
                CreateBasketLine(basketItem);
            }

            UpdatedOn = DateTime.UtcNow;
        }

        public void RemoveAllItemFromBasket()
        {
            while (_basketLines.Any())
            {
                RemoveBasketLine(_basketLines.First());
            }
        }

        public void RemoveItemFromBasket(string productId)
        {
            var existBasketLine = _basketLines.FirstOrDefault(line => line.BasketItem.ProductId == productId);
            if (existBasketLine == null) throw new ItemNotFoundInBasketException(productId);
            RemoveBasketLine(existBasketLine);
        }

        private void RemoveBasketLine(BasketLine basketLine)
        {
            basketLine.Discard();
            _basketLines.Remove(basketLine);

            UpdatedOn = DateTime.UtcNow;
        }
        
        private void CreateBasketLine(BasketItem basketItem)
        {
            BasketLine basketLine = BasketLine.Create(this, basketItem);
            _basketLines.Add(basketLine);
        }
    }
}