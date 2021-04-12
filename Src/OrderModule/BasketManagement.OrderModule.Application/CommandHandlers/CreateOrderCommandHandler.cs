using System.Threading;
using System.Threading.Tasks;
using BasketManagement.OrderModule.Application.Commands;
using BasketManagement.OrderModule.Domain;
using BasketManagement.OrderModule.Domain.Repositories;
using BasketManagement.OrderModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.OrderModule.Application.CommandHandlers
{
    public class CreateOrderCommandHandler : IDomainCommandHandler<CreateOrderCommand, OrderId>
    {
        private readonly IOrderDbContext _orderDbContext;

        public CreateOrderCommandHandler(IOrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<OrderId> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            IOrderRepository orderRepository = _orderDbContext.OrderRepository;

            Order order = Order.Create(request.AccountId, request.OrderItems);
            await orderRepository.AddAsync(order, cancellationToken);

            return order.Id;
        }
    }
}