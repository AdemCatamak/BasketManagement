using System.Threading;
using System.Threading.Tasks;
using BasketManagement.ProductModule.Application.Commands;
using BasketManagement.ProductModule.Domain;
using BasketManagement.ProductModule.Domain.Repositories;
using BasketManagement.ProductModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.ProductModule.Application.CommandHandlers
{
    public class CreateBookCommandHandler : IDomainCommandHandler<CreateBookCommand, BookId>
    {
        private readonly IProductDbContext _productDbContext;

        public CreateBookCommandHandler(IProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public async Task<BookId> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var product = Book.Create(request.BookName, request.AuthorName);

            IBookRepository bookRepository = _productDbContext.BookRepository;
            await bookRepository.AddAsync(product, cancellationToken);

            return product.Id;
        }
    }
}