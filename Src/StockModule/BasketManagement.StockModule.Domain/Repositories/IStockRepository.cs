using System.Threading;
using System.Threading.Tasks;
using BasketManagement.Shared.Domain;
using BasketManagement.Shared.Domain.Pagination;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.StockModule.Domain.Repositories
{
    public interface IStockRepository
    {
        Task AddAsync(Stock stock, CancellationToken cancellationToken);
        public Task<Stock> GetFirstAsync(IExpressionSpecification<Stock> specification, CancellationToken cancellationToken);
        public Task<PaginatedCollection<Stock>> GetAsync(IExpressionSpecification<Stock> specification, int offset, int limit, CancellationToken cancellationToken);
        public Task<PaginatedCollection<Stock>> GetAsync(IExpressionSpecification<Stock> specification, int offset, int limit, OrderBy<Stock> orderBy, CancellationToken cancellationToken);
    }
}