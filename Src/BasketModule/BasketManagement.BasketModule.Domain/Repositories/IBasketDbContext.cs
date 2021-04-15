namespace BasketManagement.BasketModule.Domain.Repositories
{
    public interface IBasketDbContext
    {
        IBasketRepository BasketRepository { get; }
    }
}