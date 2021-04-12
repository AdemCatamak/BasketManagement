using BasketManagement.BasketModule.Domain;
using BasketManagement.BasketModule.Domain.Exceptions;
using BasketManagement.BasketModule.Domain.Services;
using Stateless;

namespace BasketManagement.BasketModule.Application.Services
{
    public class BasketStateMachine : IBasketStateMachine
    {
        public BasketStatuses CurrentStatus { get; }
        public Basket Basket { get; }

        private readonly StateMachine<BasketStatuses, BasketStatuses> _stateMachine;

        public BasketStateMachine(Basket basket)
        {
            Basket = basket;
            CurrentStatus = basket.BasketStatus;

            _stateMachine = new StateMachine<BasketStatuses, BasketStatuses>(basket.BasketStatus);

            _stateMachine.Configure(BasketStatuses.Submitted)
                         .Permit(BasketStatuses.OrderNotFulfilled, BasketStatuses.OrderNotFulfilled);

            _stateMachine.Configure(BasketStatuses.Submitted)
                         .Permit(BasketStatuses.OrderFulfilled, BasketStatuses.OrderFulfilled);

            _stateMachine.Configure(BasketStatuses.OrderFulfilled)
                         .Permit(BasketStatuses.Shipped, BasketStatuses.Shipped);

            _stateMachine.OnUnhandledTrigger((states, actions) => throw new InvalidStatusTransitionException(states, actions));
        }


        public void ChangeBasketStatus(BasketStatuses targetBasketStatus)
        {
            _stateMachine.Fire(targetBasketStatus);
        }
    }
}