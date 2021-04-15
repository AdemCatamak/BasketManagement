using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.BasketModule.Application.Commands
{
    public class DeleteBasketCommand : IDomainCommand
    {
        public string AccountId { get; private set; }
        public BasketId BasketId { get; private set; }

        public DeleteBasketCommand(string accountId, BasketId basketId)
        {
            AccountId = accountId;
            BasketId = basketId;
        }
    }
}