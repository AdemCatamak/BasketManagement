using System.Threading;
using System.Threading.Tasks;
using BasketManagement.Shared.Domain;
using BasketManagement.Shared.Domain.Pagination;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.ProductModule.Domain.Repositories
{
    public interface IBookRepository
    {
        Task AddAsync(Book book, CancellationToken cancellationToken);

        public Task<Book> GetFirstAsync(IExpressionSpecification<Book> specification, CancellationToken cancellationToken);
        public Task<PaginatedCollection<Book>> GetAsync(IExpressionSpecification<Book> specification, int offset, int limit, CancellationToken cancellationToken);
        public Task<PaginatedCollection<Book>> GetAsync(IExpressionSpecification<Book> specification, int offset, int limit, OrderBy<Book> orderBy, CancellationToken cancellationToken);
    }
}