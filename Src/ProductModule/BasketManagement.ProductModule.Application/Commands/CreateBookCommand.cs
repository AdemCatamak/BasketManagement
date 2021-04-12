using BasketManagement.ProductModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.ProductModule.Application.Commands
{
    public class CreateBookCommand : IDomainCommand<BookId>
    {
        public string BookName { get; private set; }
        public string AuthorName { get; private set; }

        public CreateBookCommand(string bookName, string authorName)
        {
            BookName = bookName;
            AuthorName = authorName;
        }
    }
}