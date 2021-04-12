using System.Threading;
using System.Threading.Tasks;
using BasketManagement.ProductModule.Contracts.IntegrationEvents;
using BasketManagement.ProductModule.Domain;
using BasketManagement.ProductModule.Domain.DomainEvents;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Domain.Outbox;

namespace BasketManagement.ProductModule.Application.DomainEventHandlers
{
    public class BookCreatedEvent_PublishIntegrationEvent : IDomainEventHandler<BookCreatedEvent>
    {
        private readonly IOutboxClient _outboxClient;

        public BookCreatedEvent_PublishIntegrationEvent(IOutboxClient outboxClient)
        {
            _outboxClient = outboxClient;
        }

        public async Task Handle(BookCreatedEvent notification, CancellationToken cancellationToken)
        {
            Book book = notification.Book;
            BookCreatedIntegrationEvent bookCreatedIntegrationEvent = new BookCreatedIntegrationEvent(book.Id.Value.ToString(),
                                                                                                      book.BookName,
                                                                                                      book.AuthorName,
                                                                                                      book.CreatedOn);

            await _outboxClient.AddAsync(bookCreatedIntegrationEvent, cancellationToken);
        }
    }
}