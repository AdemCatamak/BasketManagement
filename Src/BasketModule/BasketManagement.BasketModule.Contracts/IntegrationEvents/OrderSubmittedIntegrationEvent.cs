using System.Collections.Generic;
using BasketManagement.BasketModule.Domain;
using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.IIntegrationMessages;

namespace BasketManagement.BasketModule.Contracts.IntegrationEvents
{
    public class OrderSubmittedIntegrationEvent : IIntegrationEvent
    {
        public BasketId BasketId { get; private set; }
        public BasketStatuses BasketStatus => BasketStatuses.Submitted;
        public List<BasketItem> OrderItems { get; private set; }

        public OrderSubmittedIntegrationEvent(BasketId basketId, List<BasketItem> orderItems)
        {
            BasketId = basketId;
            OrderItems = orderItems;
        }
    }
}