using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.ProductModule.Domain.DomainEvents
{
    public class BookCreatedEvent : IDomainEvent
    {
        public Book Book { get; private set; }

        public BookCreatedEvent(Book book)
        {
            Book = book;
        }
    }
}