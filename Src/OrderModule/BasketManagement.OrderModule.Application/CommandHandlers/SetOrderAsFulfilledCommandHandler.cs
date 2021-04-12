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
    public class SetOrderAsFulfilledCommandHandler : IDomainCommandHandler<SetOrderAsFulfilledCommand>
    {
        private readonly IOrderStateMachineFactory _orderStateMachineFactory;
        private readonly IOrderDbContext _orderDbContext;

        public SetOrderAsFulfilledCommandHandler(IOrderStateMachineFactory orderStateMachineFactory, IOrderDbContext orderDbContext)
        {
            _orderStateMachineFactory = orderStateMachineFactory;
            _orderDbContext = orderDbContext;
        }

        public async Task<bool> Handle(SetOrderAsFulfilledCommand request, CancellationToken cancellationToken)
        {
            ExpressionSpecification<Order> specification = new OrderIdIs(request.OrderId);

            var orderRepository = _orderDbContext.OrderRepository;
            Order order = await orderRepository.GetFirstAsync(specification, cancellationToken);

            IOrderStateMachine orderStateMachine = _orderStateMachineFactory.Generate(order);

            order.ChangeOrderStatus(orderStateMachine, OrderStatuses.OrderFulfilled);

            return true;
        }
    }
}