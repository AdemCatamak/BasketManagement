using System.Threading;
using System.Threading.Tasks;
using BasketManagement.OrderModule.Application.Commands;
using BasketManagement.OrderModule.Domain;
using BasketManagement.OrderModule.Domain.Repositories;
using BasketManagement.OrderModule.Domain.Services;
using BasketManagement.OrderModule.Domain.Specifications;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.OrderModule.Application.CommandHandlers
{
    public class SetOrderAsShippedCommandHandler : IDomainCommandHandler<SetOrderAsShippedCommand>
    {
        private readonly IOrderDbContext _orderDbContext;
        private readonly IOrderStateMachineFactory _orderStateMachineFactory;

        public SetOrderAsShippedCommandHandler(IOrderDbContext orderDbContext, IOrderStateMachineFactory orderStateMachineFactory)
        {
            _orderDbContext = orderDbContext;
            _orderStateMachineFactory = orderStateMachineFactory;
        }

        public async Task<bool> Handle(SetOrderAsShippedCommand request, CancellationToken cancellationToken)
        {
            ExpressionSpecification<Order> specification = new OrderIdIs(request.OrderId);

            var orderRepository = _orderDbContext.OrderRepository;
            Order order = await orderRepository.GetFirstAsync(specification, cancellationToken);

            IOrderStateMachine orderStateMachine = _orderStateMachineFactory.Generate(order);

            order.ChangeOrderStatus(orderStateMachine, OrderStatuses.Shipped);

            return true;
        }
    }
}