namespace BasketManagement.OrderModule.Domain.Repositories
{
    public interface IOrderDbContext
    {
        IOrderRepository OrderRepository { get; }
    }
}