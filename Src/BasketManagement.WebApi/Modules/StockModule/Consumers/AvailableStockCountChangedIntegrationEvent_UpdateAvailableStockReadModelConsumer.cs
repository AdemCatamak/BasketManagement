using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using BasketManagement.Shared.Infrastructure;
using BasketManagement.StockModule.Application.Commands;
using BasketManagement.StockModule.Contracts;

namespace BasketManagement.WebApi.Modules.StockModule.Consumers
{
    public class AvailableStockCountChangedIntegrationEvent_UpdateAvailableStockReadModelConsumer
        : IConsumer<AvailableStockCountChangedIntegrationEvent>
    {
        private readonly IExecutionContext _executionContext;

        public AvailableStockCountChangedIntegrationEvent_UpdateAvailableStockReadModelConsumer(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        public async Task Consume(ConsumeContext<AvailableStockCountChangedIntegrationEvent> context)
        {
            AvailableStockCountChangedIntegrationEvent availableStockCountChangedIntegrationEvent = context.Message;
            var updateAvailableStockCountReadModelCommand = new UpdateAvailableStockCountReadModelCommand(availableStockCountChangedIntegrationEvent.ProductId,
                                                                                                          availableStockCountChangedIntegrationEvent.AvailableStockCount,
                                                                                                          availableStockCountChangedIntegrationEvent.ChangedOn,
                                                                                                          availableStockCountChangedIntegrationEvent.ActionId);

            await _executionContext.ExecuteAsync(updateAvailableStockCountReadModelCommand, CancellationToken.None);
        }
    }

    public class AvailableStockCountChangedIntegrationEvent_UpdateAvailableStockReadModelConsumer_Definition
        : ConsumerDefinition<AvailableStockCountChangedIntegrationEvent_UpdateAvailableStockReadModelConsumer>
    {
        public AvailableStockCountChangedIntegrationEvent_UpdateAvailableStockReadModelConsumer_Definition()
        {
            EndpointName = "StockModule.AvailableStockCountChangedIntegrationEvent_UpdateAvailableStockReadModelConsumer";
        }
    }
}