using System.Collections.Generic;
using BasketManagement.OrderModule.Domain;
using BasketManagement.OrderModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Domain.Pagination;

namespace BasketManagement.OrderModule.Application.Commands
{
    public class QueryOrderCommand : PaginatedRequest,
                                     IDomainCommand<PaginatedCollection<OrderResponse>>
    {
        public string AccountId { get; set; }

        public QueryOrderCommand(string accountId)
        {
            AccountId = accountId;
        }
    }

    public class OrderResponse
    {
        public OrderId OrderId { get; private set; }
        public OrderStatuses OrderStatus { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        public OrderResponse(OrderId orderId, OrderStatuses orderStatus, List<OrderItem> orderItems)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
            OrderItems = orderItems;
        }
    }
}