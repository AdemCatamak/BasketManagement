namespace BasketManagement.BasketModule.Domain.Services
{
    public interface IBasketStateMachine
    {
        BasketStatuses CurrentStatus { get; }
        Basket Basket { get; }

        void ChangeBasketStatus(BasketStatuses targetBasketStatus);
    }
}