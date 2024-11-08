using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MessageStorage;
using BasketManagement.Shared.Domain.IIntegrationMessages;

namespace BasketManagement.Shared.Infrastructure.Outbox
{
    public class IntegrationEventHandler : Handler<IIntegrationEvent>
    {
        private readonly IBusControl _busControl;

        public IntegrationEventHandler(IBusControl busControl)
        {
            _busControl = busControl;
        }

        protected override async Task HandleAsync(IIntegrationEvent payload, CancellationToken cancellationToken)
        {
            await _busControl.Publish(payload, payload.GetType(), cancellationToken);
        }
    }
}