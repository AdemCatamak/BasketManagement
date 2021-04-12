using System.Threading;
using System.Threading.Tasks;
using BasketManagement.BasketModule.Domain.Events;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.BasketModule.Application.DomainEventHandlers
{
    public class BasketLineCreated_RemoveFromStock : IDomainEventHandler<BasketLineCreatedEvent>
    {
        public Task Handle(BasketLineCreatedEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}