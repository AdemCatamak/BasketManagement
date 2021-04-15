using MediatR;

namespace BasketManagement.Shared.Domain.DomainMessageBroker
{
    public interface IDomainCommand : IDomainCommand<bool>
    {
    }

    public interface IDomainCommand<out TResponse> : IDomainMessage, IRequest<TResponse>
    {
    }
}