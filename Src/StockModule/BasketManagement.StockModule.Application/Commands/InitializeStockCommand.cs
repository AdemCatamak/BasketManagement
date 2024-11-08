using System;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.StockModule.Application.Commands
{
    public class InitializeStockCommand : IDomainCommand<Guid>
    {
        public string ProductId { get; private set; }
        public string CorrelationId { get; private set; }
        public int AvailableStock { get; private set; }

        public InitializeStockCommand(string productId, string correlationId, int availableStock)
        {
            ProductId = productId;
            CorrelationId = correlationId;
            AvailableStock = availableStock;
        }
    }
}