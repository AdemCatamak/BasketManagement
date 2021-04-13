using BasketManagement.Shared.Domain.Pagination;

namespace BasketManagement.WebApi.Modules.StockModule.Controllers.Requests
{
    public class GetStockHttpRequest : PaginatedRequest
    {
        public string? ProductId { get; set; } = null;
        public bool? InStock { get; set; } = null;
    }
}