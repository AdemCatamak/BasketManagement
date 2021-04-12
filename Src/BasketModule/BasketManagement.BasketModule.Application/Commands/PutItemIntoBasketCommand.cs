using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.BasketModule.Application.Commands
{
    public class PutItemIntoBasketCommand : IDomainCommand
    {
        public string AccountId { get; private set; }
        public BasketId BasketId  { get; private set; }
        public BasketItem BasketItem { get; private set; }

        public PutItemIntoBasketCommand(string accountId, BasketId basketId, BasketItem basketItem)
        {
            AccountId = accountId;
            BasketId = basketId;
            BasketItem = basketItem;
        }
    }
}