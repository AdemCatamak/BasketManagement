using BasketManagement.Shared.Domain.Pagination;

namespace BasketManagement.WebApi.Modules.ProductModule.Controllers.Requests
{
    public class GetBookCollectionHttpRequest : PaginatedRequest
    {
        public string? PartialBookName { get; set; } = null;
        public string? PartialAuthorName { get; set; } = null;
    }
}