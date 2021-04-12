namespace BasketManagement.AccountModule.Domain.Repositories
{
    public interface IAccountDbContext
    {
        IAccountRepository AccountRepository { get; }
    }
}