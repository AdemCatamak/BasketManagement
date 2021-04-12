using MediatR;

namespace BasketManagement.Shared.Domain.DomainMessageBroker
{
    public interface IDomainEvent : IDomainMessage, INotification
    {
    }
}