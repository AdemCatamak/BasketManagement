using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.BasketModule.Application.Commands
{
    public class CreateBasketCommand : IDomainCommand<BasketId>
    {
        public string AccountId { get; private set; }

        public CreateBasketCommand(string accountId)
        {
            AccountId = accountId;
        }
    }
}