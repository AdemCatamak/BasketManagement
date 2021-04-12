using System.Threading;
using System.Threading.Tasks;
using BasketManagement.BasketModule.Domain.Events;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.BasketModule.Application.DomainEventHandlers
{
    public class BasketLineQuantityIncreased_RemoveFromStock : IDomainEventHandler<BasketLineQuantityIncreasedEvent>
    {
        public Task Handle(BasketLineQuantityIncreasedEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}