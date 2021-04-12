using System.Threading;
using System.Threading.Tasks;
using BasketManagement.BasketModule.Domain.Events;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.BasketModule.Application.DomainEventHandlers
{
    public class BasketLineQuantityDecreased_AddToStock : IDomainEventHandler<BasketLineQuantityDecreasedEvent>
    {
        public Task Handle(BasketLineQuantityDecreasedEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}