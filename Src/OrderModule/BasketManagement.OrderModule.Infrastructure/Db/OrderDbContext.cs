using BasketManagement.OrderModule.Domain.Repositories;
using BasketManagement.OrderModule.Infrastructure.Db.Repositories;
using BasketManagement.Shared.Infrastructure.Db;

namespace BasketManagement.OrderModule.Infrastructure.Db
{
    public class OrderDbContext : IOrderDbContext
    {
        private readonly EfAppDbContext _dbContext;

        public OrderDbContext(EfAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IOrderRepository OrderRepository => new OrderRepository(_dbContext);
    }
}