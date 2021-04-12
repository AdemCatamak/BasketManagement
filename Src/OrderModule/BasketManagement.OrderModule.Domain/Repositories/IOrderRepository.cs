using System.Threading;
using System.Threading.Tasks;
using BasketManagement.Shared.Domain;
using BasketManagement.Shared.Domain.Pagination;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.OrderModule.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order, CancellationToken cancellationToken);
        Task<Order> GetFirstAsync(IExpressionSpecification<Order> specification, CancellationToken cancellationToken);
        Task<PaginatedCollection<Order>> GetAsync(IExpressionSpecification<Order> specification, int offset, int limit, CancellationToken cancellationToken);
        Task<PaginatedCollection<Order>> GetAsync(IExpressionSpecification<Order> specification, int offset, int limit, OrderBy<Order> orderBy, CancellationToken cancellationToken);
    }
}