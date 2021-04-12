using BasketManagement.OrderModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.OrderModule.Application.Commands
{
    public class SetOrderAsNotFulfilledCommand : IDomainCommand
    {
        public OrderId OrderId { get; private set; }

        public SetOrderAsNotFulfilledCommand(OrderId orderId)
        {
            OrderId = orderId;
        }
    }
}