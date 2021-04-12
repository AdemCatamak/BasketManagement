using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BasketManagement.ProductModule.Application.Commands;
using BasketManagement.ProductModule.Domain;
using BasketManagement.ProductModule.Domain.Repositories;
using BasketManagement.ProductModule.Domain.Specifications;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Domain.Pagination;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.SpecificationOperations;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.ProductModule.Application.CommandHandlers
{
    public class QueryBookCommandHandler : IDomainCommandHandler<QueryBookCommand, PaginatedCollection<BookResponse>>
    {
        private readonly IProductDbContext _productDbContext;

        public QueryBookCommandHandler(IProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public async Task<PaginatedCollection<BookResponse>> Handle(QueryBookCommand request, CancellationToken cancellationToken)
        {
            IExpressionSpecification<Book> specification = new BookIsRemoved().Not();

            if (!string.IsNullOrEmpty(request.PartialBookName))
            {
                specification = specification.And(new BookNameContains(request.PartialBookName));
            }

            if (!string.IsNullOrEmpty(request.PartialAuthorName))
            {
                specification = specification.And(new AuthorNameContains(request.PartialAuthorName));
            }

            IBookRepository bookRepository = _productDbContext.BookRepository;
            PaginatedCollection<Book> paginatedCollection = await bookRepository.GetAsync(specification, request.Offset, request.Limit, cancellationToken);

            PaginatedCollection<BookResponse> result = new PaginatedCollection<BookResponse>(paginatedCollection.TotalCount,
                                                                                             paginatedCollection.Data.Select(p => new BookResponse(p.Id, p.BookName, p.AuthorName)));
            return result;
        }
    }
}