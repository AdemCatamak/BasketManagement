using System;
using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.BasketModule.Application.Commands
{
    public class RemoveItemFromBasket : IDomainCommand
    {
        public string AccountId { get; private set; }
        public BasketId BasketId { get; private set; }
        public string ProductId { get; private set; }

        public RemoveItemFromBasket(string accountId, BasketId basketId, string productId)
        {
            AccountId = accountId;
            BasketId = basketId;
            ProductId = productId;
        }
    }
}