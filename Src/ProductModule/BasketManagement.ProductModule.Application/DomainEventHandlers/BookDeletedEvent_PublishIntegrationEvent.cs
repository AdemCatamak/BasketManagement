using System.Threading;
using System.Threading.Tasks;
using BasketManagement.ProductModule.Contracts.IntegrationEvents;
using BasketManagement.ProductModule.Domain;
using BasketManagement.ProductModule.Domain.DomainEvents;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Domain.Outbox;

namespace BasketManagement.ProductModule.Application.DomainEventHandlers
{
    public class BookDeletedEvent_PublishIntegrationEvent : IDomainEventHandler<BookDeletedEvent>
    {
        private readonly IOutboxClient _outboxClient;

        public BookDeletedEvent_PublishIntegrationEvent(IOutboxClient outboxClient)
        {
            _outboxClient = outboxClient;
        }

        public async Task Handle(BookDeletedEvent notification, CancellationToken cancellationToken)
        {
            Book book = notification.Book;
            BookDeletedIntegrationEvent bookDeletedIntegrationEvent
                = new BookDeletedIntegrationEvent(book.Id.Value.ToString(),
                                                  book.BookName,
                                                  book.AuthorName,
                                                  book.UpdatedOn);

            await _outboxClient.AddAsync(bookDeletedIntegrationEvent, cancellationToken);
        }
    }
}