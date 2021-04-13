using System;
using BasketManagement.Shared.Domain.Pagination;

namespace BasketManagement.WebApi.Modules.BasketModule.Controllers.Requests
{
    public class GetBasketHttpRequest : PaginatedRequest
    {
        public Guid? BasketId { get;  set; }
    }
}