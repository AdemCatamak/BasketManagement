using BasketManagement.OrderModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.OrderModule.Application.Commands
{
    public class SetOrderAsShippedCommand : IDomainCommand
    {
        public OrderId OrderId { get; private set; }

        public SetOrderAsShippedCommand(OrderId orderId)
        {
            OrderId = orderId;
        }
    }
}