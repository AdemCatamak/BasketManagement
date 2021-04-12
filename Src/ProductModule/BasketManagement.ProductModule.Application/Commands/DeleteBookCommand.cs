using BasketManagement.ProductModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.ProductModule.Application.Commands
{
    public class DeleteBookCommand : IDomainEvent, IDomainCommand
    {
        public BookId BookId { get; }

        public DeleteBookCommand(BookId bookId)
        {
            BookId = bookId;
        }
    }
}