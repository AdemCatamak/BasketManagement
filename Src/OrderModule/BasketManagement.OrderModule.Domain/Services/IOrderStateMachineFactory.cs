namespace BasketManagement.OrderModule.Domain.Services
{
    public interface IOrderStateMachineFactory
    {
        IOrderStateMachine Generate(Order order);
    }
}