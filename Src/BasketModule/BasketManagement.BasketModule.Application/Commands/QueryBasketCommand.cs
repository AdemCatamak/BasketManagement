using System.Collections.Generic;
using BasketManagement.BasketModule.Domain;
using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Domain.Pagination;

namespace BasketManagement.BasketModule.Application.Commands
{
    public class QueryBasketCommand : PaginatedRequest,
                                     IDomainCommand<PaginatedCollection<BasketResponse>>
    {
        public string AccountId { get; set; }

        public QueryBasketCommand(string accountId)
        {
            AccountId = accountId;
        }
    }

    public class BasketResponse
    {
        public BasketId BasketId { get; private set; }
        public BasketStatuses BasketStatus { get; private set; }
        public List<BasketItem> OrderItems { get; private set; }

        public BasketResponse(BasketId basketId, BasketStatuses basketStatus, List<BasketItem> orderItems)
        {
            BasketId = basketId;
            BasketStatus = basketStatus;
            OrderItems = orderItems;
        }
    }
}