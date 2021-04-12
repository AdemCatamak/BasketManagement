using System.Threading;
using System.Threading.Tasks;
using BasketManagement.ProductModule.Application.Commands;
using BasketManagement.ProductModule.Domain;
using BasketManagement.ProductModule.Domain.Repositories;
using BasketManagement.ProductModule.Domain.Specifications;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.SpecificationOperations;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.ProductModule.Application.CommandHandlers
{
    public class DeleteBookCommandHandler : IDomainCommandHandler<DeleteBookCommand>
    {
        private readonly IProductDbContext _productDbContext;

        public DeleteBookCommandHandler(IProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            IExpressionSpecification<Book> specification = new BookIdIs(request.BookId)
               .And(ExpressionSpecificationNotOperatorExtension.Not(new BookIsRemoved()));

            IBookRepository bookRepository = _productDbContext.BookRepository;
            Book book = await bookRepository.GetFirstAsync(specification, cancellationToken);
            book.SetDeleted();
            return true;
        }
    }
}