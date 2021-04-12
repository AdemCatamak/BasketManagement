using System.Threading;
using System.Threading.Tasks;
using BasketManagement.BasketModule.Application.Services;
using BasketManagement.BasketModule.Domain.Events;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.StockModule.Application.Commands;

namespace BasketManagement.BasketModule.Application.DomainEventHandlers
{
    public class BasketLineQuantityIncreased_RemoveFromStock : IDomainEventHandler<BasketLineQuantityIncreasedEvent>
    {
        private readonly IDomainMessageBroker _domainMessageBroker;
        private readonly IStockActionCorrelationIdGenerator _stockActionCorrelationIdGenerator;

        public BasketLineQuantityIncreased_RemoveFromStock(IDomainMessageBroker domainMessageBroker, IStockActionCorrelationIdGenerator stockActionCorrelationIdGenerator)
        {
            _domainMessageBroker = domainMessageBroker;
            _stockActionCorrelationIdGenerator = stockActionCorrelationIdGenerator;
        }

        public async Task Handle(BasketLineQuantityIncreasedEvent notification, CancellationToken cancellationToken)
        {
            var basketLine = notification.BasketLine;
            int oldQuantity = notification.OldQuantity;

            int gap = basketLine.BasketItem.Quantity - oldQuantity;

            string correlationId =_stockActionCorrelationIdGenerator.Generate(basketLine);
            RemoveFromStockCommand removeFromStockCommand = new RemoveFromStockCommand(basketLine.BasketItem.ProductId, gap, correlationId);
            await _domainMessageBroker.SendAsync(removeFromStockCommand, cancellationToken);
        }
    }
}