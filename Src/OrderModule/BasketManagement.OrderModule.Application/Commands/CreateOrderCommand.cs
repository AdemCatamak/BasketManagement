using System.Collections.Generic;
using BasketManagement.OrderModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.OrderModule.Application.Commands
{
    public class CreateOrderCommand : IDomainCommand<OrderId>
    {
        public List<OrderItem> OrderItems { get; private set; }
        public string AccountId { get; private set; }

        public CreateOrderCommand(string accountId, List<OrderItem> orderItems)
        {
            AccountId = accountId;
            OrderItems = orderItems;
        }
    }
}