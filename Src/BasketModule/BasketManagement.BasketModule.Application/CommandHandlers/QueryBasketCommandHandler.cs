using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BasketManagement.BasketModule.Application.Commands;
using BasketManagement.BasketModule.Domain;
using BasketManagement.BasketModule.Domain.Repositories;
using BasketManagement.BasketModule.Domain.Specifications;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Domain.Pagination;

namespace BasketManagement.BasketModule.Application.CommandHandlers
{
    public class QueryBasketCommandHandler : IDomainCommandHandler<QueryBasketCommand, PaginatedCollection<BasketResponse>>
    {
        private readonly IBasketDbContext _basketDbContext;

        public QueryBasketCommandHandler(IBasketDbContext basketDbContext)
        {
            _basketDbContext = basketDbContext;
        }

        public async Task<PaginatedCollection<BasketResponse>> Handle(QueryBasketCommand request, CancellationToken cancellationToken)
        {
            var specification = new AccountIdIs(request.AccountId);

            IBasketRepository basketRepository = _basketDbContext.BasketRepository;
            PaginatedCollection<Basket> order = await basketRepository.GetAsync(specification, request.Offset, request.Limit, cancellationToken);

            PaginatedCollection<BasketResponse> result = new PaginatedCollection<BasketResponse>(order.TotalCount,
                                                                                               order.Data
                                                                                                    .Select(x => new BasketResponse(x.Id, x.BasketStatus, x.BasketLines.Select(line => line.BasketItem).ToList())));

            return result;
        }
    }
}