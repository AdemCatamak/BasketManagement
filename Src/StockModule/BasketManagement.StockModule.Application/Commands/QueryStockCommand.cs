using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Domain.Pagination;

namespace BasketManagement.StockModule.Application.Commands
{
    public class QueryStockCommand : PaginatedRequest,
                                     IDomainCommand<PaginatedCollection<StockResponse>>
    {
        public string? ProductId { get; set; }
        public bool? InStock { get; set; }
    }

    public class StockResponse
    {
        public string ProductId { get; private set; }
        public int AvailableStockCount { get; private set; }

        public StockResponse(string productId, int availableStockCount)
        {
            ProductId = productId;
            AvailableStockCount = availableStockCount;
        }
    }
}