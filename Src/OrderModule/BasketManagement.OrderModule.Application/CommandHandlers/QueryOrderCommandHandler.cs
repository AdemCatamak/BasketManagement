using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BasketManagement.OrderModule.Application.Commands;
using BasketManagement.OrderModule.Domain;
using BasketManagement.OrderModule.Domain.Repositories;
using BasketManagement.OrderModule.Domain.Specifications;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Domain.Pagination;

namespace BasketManagement.OrderModule.Application.CommandHandlers
{
    public class QueryOrderCommandHandler : IDomainCommandHandler<QueryOrderCommand, PaginatedCollection<OrderResponse>>
    {
        private readonly IOrderDbContext _orderDbContext;

        public QueryOrderCommandHandler(IOrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<PaginatedCollection<OrderResponse>> Handle(QueryOrderCommand request, CancellationToken cancellationToken)
        {
            var specification = new AccountIdIs(request.AccountId);

            IOrderRepository orderRepository = _orderDbContext.OrderRepository;
            PaginatedCollection<Order> order = await orderRepository.GetAsync(specification, request.Offset, request.Limit, cancellationToken);

            PaginatedCollection<OrderResponse> result = new PaginatedCollection<OrderResponse>(order.TotalCount,
                                                                                               order.Data
                                                                                                    .Select(x => new OrderResponse(x.Id, x.OrderStatus, x.OrderLines.Select(line => line.OrderItem).ToList())));

            return result;
        }
    }
}