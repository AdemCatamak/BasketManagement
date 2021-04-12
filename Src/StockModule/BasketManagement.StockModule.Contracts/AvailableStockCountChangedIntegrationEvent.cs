using System;
using BasketManagement.Shared.Domain.IIntegrationMessages;

namespace BasketManagement.StockModule.Contracts
{
    public class AvailableStockCountChangedIntegrationEvent : IIntegrationEvent
    {
        public string ProductId { get; private set; }
        public int AvailableStockCount { get; private set; }
        public DateTime ChangedOn { get; private set; }
        public Guid ActionId { get; private set; }

        public AvailableStockCountChangedIntegrationEvent(string productId, int availableStockCount, DateTime changedOn, Guid actionId)
        {
            ProductId = productId;
            AvailableStockCount = availableStockCount;
            ChangedOn = changedOn;
            ActionId = actionId;
        }
    }
}