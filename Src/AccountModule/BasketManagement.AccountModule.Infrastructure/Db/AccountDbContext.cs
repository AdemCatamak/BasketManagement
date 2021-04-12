using BasketManagement.AccountModule.Domain.Repositories;
using BasketManagement.AccountModule.Infrastructure.Db.Repositories;
using BasketManagement.Shared.Infrastructure.Db;

namespace BasketManagement.AccountModule.Infrastructure.Db
{
    public class AccountDbContext : IAccountDbContext
    {
        private readonly EfAppDbContext _efAppDbContext;

        public AccountDbContext(EfAppDbContext efAppDbContext)
        {
            _efAppDbContext = efAppDbContext;
        }

        public IAccountRepository AccountRepository => new AccountRepository(_efAppDbContext);
    }
}