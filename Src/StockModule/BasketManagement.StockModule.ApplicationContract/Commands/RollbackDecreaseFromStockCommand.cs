using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.StockModule.Application.Commands
{
    public class RollbackDecreaseFromStockCommand : IDomainCommand
    {
        public string CorrelationId { get; private set; }

        public RollbackDecreaseFromStockCommand(string correlationId)
        {
            CorrelationId = correlationId;
        }
    }
}