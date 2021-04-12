namespace BasketManagement.ProductModule.Domain.Repositories
{
    public interface IProductDbContext
    {
        IBookRepository BookRepository { get; }
    }
}