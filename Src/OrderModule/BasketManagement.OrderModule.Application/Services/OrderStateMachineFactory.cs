using BasketManagement.OrderModule.Domain;
using BasketManagement.OrderModule.Domain.Services;

namespace BasketManagement.OrderModule.Application.Services
{
    public class OrderStateMachineFactory : IOrderStateMachineFactory
    {
        public IOrderStateMachine Generate(Order order)
        {
            OrderStateMachine orderStateMachine = new OrderStateMachine(order);
            return orderStateMachine;
        }
    }
}