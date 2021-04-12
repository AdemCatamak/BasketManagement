using MediatR;

namespace BasketManagement.Shared.Domain.DomainMessageBroker
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IDomainEvent
    {
    }
}