using System.Collections.Generic;
using BasketManagement.OrderModule.Domain;
using BasketManagement.OrderModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.IIntegrationMessages;

namespace BasketManagement.OrderModule.Contracts.IntegrationEvents
{
    public class OrderSubmittedIntegrationEvent : IIntegrationEvent
    {
        public OrderId OrderId { get; private set; }
        public OrderStatuses OrderStatus => OrderStatuses.Submitted;
        public List<OrderItem> OrderItems { get; private set; }

        public OrderSubmittedIntegrationEvent(OrderId orderId, List<OrderItem> orderItems)
        {
            OrderId = orderId;
            OrderItems = orderItems;
        }
    }
}