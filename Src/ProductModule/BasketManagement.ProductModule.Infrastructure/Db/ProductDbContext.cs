using BasketManagement.ProductModule.Domain.Repositories;
using BasketManagement.ProductModule.Infrastructure.Db.Repositories;
using BasketManagement.Shared.Infrastructure.Db;

namespace BasketManagement.ProductModule.Infrastructure.Db
{
    public class ProductDbContext : IProductDbContext
    {
        private readonly EfAppDbContext _appDbContext;

        public ProductDbContext(EfAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IBookRepository BookRepository => new BookRepository(_appDbContext);
    }
}