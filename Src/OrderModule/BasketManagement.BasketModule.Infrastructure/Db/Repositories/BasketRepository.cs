using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BasketManagement.BasketModule.Domain;
using BasketManagement.BasketModule.Domain.Exceptions;
using BasketManagement.BasketModule.Domain.Repositories;
using BasketManagement.Shared.Domain;
using BasketManagement.Shared.Domain.Pagination;
using BasketManagement.Shared.Infrastructure.Db;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;
using Microsoft.EntityFrameworkCore;

namespace BasketManagement.BasketModule.Infrastructure.Db.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly EfAppDbContext _appDbContext;

        public BasketRepository(EfAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Basket basket, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(basket, cancellationToken);
        }

        public async Task<Basket> GetFirstAsync(IExpressionSpecification<Basket> specification, CancellationToken cancellationToken)
        {
            var paginatedCollection = await GetAsync(specification, 0, 1, cancellationToken);
            return paginatedCollection.Data.First();
        }

        public Task<PaginatedCollection<Basket>> GetAsync(IExpressionSpecification<Basket> specification, int offset, int limit, CancellationToken cancellationToken)
        {
            return GetAsync(specification, offset, limit, OrderBy<Basket>.Asc(x => x.CreatedOn), cancellationToken);
        }

        public async Task<PaginatedCollection<Basket>> GetAsync(IExpressionSpecification<Basket> specification, int offset, int limit, OrderBy<Basket> orderBy, CancellationToken cancellationToken = default)
        {
            IQueryable<Basket> baskets = _appDbContext.Set<Basket>()
                                                   .Include(order => order.BasketLines)
                                                   .Where(specification.Expression);

            baskets = orderBy.Apply(baskets);

            (int totalCount, List<Basket> basketList)
                = await baskets.PaginatedQueryAsync(offset, limit, cancellationToken);


            if (!basketList.Any())
            {
                throw new BasketNotFoundException();
            }

            var paginatedCollection = new PaginatedCollection<Basket>(totalCount, basketList);
            return paginatedCollection;
        }
    }
}