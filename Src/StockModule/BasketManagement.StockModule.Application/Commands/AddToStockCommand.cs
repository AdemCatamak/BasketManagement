using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.StockModule.Domain.Exceptions;

namespace BasketManagement.StockModule.Application.Commands
{
    public class AddToStockCommand : IDomainCommand
    {
        public string ProductId { get; }
        public int Count { get; }
        public string CorrelationId { get; }

        public AddToStockCommand(string productId, int count, string correlationId)
        {
            if (count <= 0)
            {
                throw new CountNotPositiveException();
            }

            correlationId = correlationId?.Trim() ?? string.Empty;
            if (correlationId == string.Empty)
            {
                throw new CorrelationIdEmptyException();
            }

            ProductId = productId;
            Count = count;
            CorrelationId = correlationId;
        }
    }
}