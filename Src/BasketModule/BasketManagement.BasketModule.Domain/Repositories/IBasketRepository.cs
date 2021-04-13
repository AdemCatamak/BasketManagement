using System.Threading;
using System.Threading.Tasks;
using BasketManagement.Shared.Domain;
using BasketManagement.Shared.Domain.Pagination;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.BasketModule.Domain.Repositories
{
    public interface IBasketRepository
    {
        Task AddAsync(Basket basket, CancellationToken cancellationToken);
        Task<Basket> GetFirstAsync(IExpressionSpecification<Basket> specification, CancellationToken cancellationToken);
        Task<PaginatedCollection<Basket>> GetAsync(IExpressionSpecification<Basket> specification, int offset, int limit, CancellationToken cancellationToken);
        Task<PaginatedCollection<Basket>> GetAsync(IExpressionSpecification<Basket> specification, int offset, int limit, OrderBy<Basket> orderBy, CancellationToken cancellationToken);
        Task RemoveAsync(Basket basket, CancellationToken cancellationToken);
    }
}