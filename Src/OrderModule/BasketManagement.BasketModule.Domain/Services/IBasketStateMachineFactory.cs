namespace BasketManagement.BasketModule.Domain.Services
{
    public interface IBasketStateMachineFactory
    {
        IBasketStateMachine Generate(Basket basket);
    }
}