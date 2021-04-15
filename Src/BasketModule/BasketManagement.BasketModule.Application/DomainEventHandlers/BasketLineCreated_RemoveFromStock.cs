using System.Threading;
using System.Threading.Tasks;
using BasketManagement.BasketModule.Application.Services;
using BasketManagement.BasketModule.Domain.Events;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.StockModule.Application.Commands;

namespace BasketManagement.BasketModule.Application.DomainEventHandlers
{
    public class BasketLineCreated_RemoveFromStock : IDomainEventHandler<BasketLineCreatedEvent>
    {
        private readonly IDomainMessageBroker _domainMessageBroker;
        private readonly IStockActionCorrelationIdGenerator _stockActionCorrelationIdGenerator;

        public BasketLineCreated_RemoveFromStock(IDomainMessageBroker domainMessageBroker, IStockActionCorrelationIdGenerator stockActionCorrelationIdGenerator)
        {
            _domainMessageBroker = domainMessageBroker;
            _stockActionCorrelationIdGenerator = stockActionCorrelationIdGenerator;
        }

        public async Task Handle(BasketLineCreatedEvent notification, CancellationToken cancellationToken)
        {
            var basketLine = notification.BasketLine;

            string correlationId =_stockActionCorrelationIdGenerator.Generate(basketLine);
            RemoveFromStockCommand removeFromStockCommand = new RemoveFromStockCommand(basketLine.BasketItem.ProductId, basketLine.BasketItem.Quantity, correlationId);
            await _domainMessageBroker.SendAsync(removeFromStockCommand, cancellationToken);
        }
    }
}