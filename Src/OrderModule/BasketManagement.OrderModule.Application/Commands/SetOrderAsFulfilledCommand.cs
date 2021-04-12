using BasketManagement.OrderModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.OrderModule.Application.Commands
{
    public class SetOrderAsFulfilledCommand : IDomainCommand
    {
        public OrderId OrderId { get; private set; }

        public SetOrderAsFulfilledCommand(OrderId orderId)
        {
            OrderId = orderId;
        }
    }
}