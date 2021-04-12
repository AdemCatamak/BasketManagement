using BasketManagement.BasketModule.Domain.Repositories;
using BasketManagement.BasketModule.Infrastructure.Db.Repositories;
using BasketManagement.Shared.Infrastructure.Db;

namespace BasketManagement.BasketModule.Infrastructure.Db
{
    public class BasketDbContext : IBasketDbContext
    {
        private readonly EfAppDbContext _dbContext;

        public BasketDbContext(EfAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IBasketRepository BasketRepository => new BasketRepository(_dbContext);
    }
}