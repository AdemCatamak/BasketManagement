using BasketManagement.BasketModule.Domain;
using BasketManagement.BasketModule.Domain.Services;

namespace BasketManagement.BasketModule.Application.Services
{
    public class BasketStateMachineFactory : IBasketStateMachineFactory
    {
        public IBasketStateMachine Generate(Basket basket)
        {
            BasketStateMachine basketStateMachine = new BasketStateMachine(basket);
            return basketStateMachine;
        }
    }
}