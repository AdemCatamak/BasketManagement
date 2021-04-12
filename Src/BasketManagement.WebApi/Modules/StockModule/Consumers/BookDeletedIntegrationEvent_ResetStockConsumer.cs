using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using BasketManagement.ProductModule.Contracts.IntegrationEvents;
using BasketManagement.Shared.Infrastructure;
using BasketManagement.StockModule.Application.Commands;

namespace BasketManagement.WebApi.Modules.StockModule.Consumers
{
    public class BookDeletedIntegrationEvent_ResetStockConsumer : IConsumer<BookDeletedIntegrationEvent>
    {
        private readonly IExecutionContext _executionContext;

        public BookDeletedIntegrationEvent_ResetStockConsumer(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        public async Task Consume(ConsumeContext<BookDeletedIntegrationEvent> context)
        {
            var bookDeletedIntegrationEvent = context.Message;
            var resetStockCommand = new ResetStockCommand(bookDeletedIntegrationEvent.BookId,
                                                          $"book-deleted-{bookDeletedIntegrationEvent.BookId}");
            await _executionContext.ExecuteAsync(resetStockCommand, CancellationToken.None);
        }
    }

    public class BookDeletedIntegrationEvent_ResetStockConsumer_Definition : ConsumerDefinition<BookDeletedIntegrationEvent_ResetStockConsumer>
    {
        public BookDeletedIntegrationEvent_ResetStockConsumer_Definition()
        {
            EndpointName = "StockModule.BookDeletedIntegrationEvent_ResetStockConsumerQueue";
        }
    }
}