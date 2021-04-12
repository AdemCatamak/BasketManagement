using System.Collections.Generic;
using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.IIntegrationMessages;

namespace BasketManagement.BasketModule.Contracts.IntegrationCommands
{
    public class OrderRollbackIntegrationCommand : IIntegrationCommand
    {
        public BasketId BasketId { get; private set; }
        public List<BasketItem> OrderItems { get; private set; }

        public OrderRollbackIntegrationCommand(BasketId basketId, List<BasketItem> orderItems)
        {
            BasketId = basketId;
            OrderItems = orderItems;
        }
    }
}