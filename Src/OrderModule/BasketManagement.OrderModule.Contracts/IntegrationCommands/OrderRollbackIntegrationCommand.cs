using System.Collections.Generic;
using BasketManagement.OrderModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.IIntegrationMessages;

namespace BasketManagement.OrderModule.Contracts.IntegrationCommands
{
    public class OrderRollbackIntegrationCommand : IIntegrationCommand
    {
        public OrderId OrderId { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        public OrderRollbackIntegrationCommand(OrderId orderId, List<OrderItem> orderItems)
        {
            OrderId = orderId;
            OrderItems = orderItems;
        }
    }
}