using System.Threading;
using System.Threading.Tasks;
using BasketManagement.BasketModule.Application.Services;
using BasketManagement.BasketModule.Domain.Events;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.StockModule.Application.Commands;

namespace BasketManagement.BasketModule.Application.DomainEventHandlers
{
    public class BasketLineQuantityDecreased_AddToStock : IDomainEventHandler<BasketLineQuantityDecreasedEvent>
    {
        private readonly IDomainMessageBroker _domainMessageBroker;
        private readonly IStockActionCorrelationIdGenerator _stockActionCorrelationIdGenerator;

        public BasketLineQuantityDecreased_AddToStock(IDomainMessageBroker domainMessageBroker, IStockActionCorrelationIdGenerator stockActionCorrelationIdGenerator)
        {
            _domainMessageBroker = domainMessageBroker;
            _stockActionCorrelationIdGenerator = stockActionCorrelationIdGenerator;
        }

        public async Task Handle(BasketLineQuantityDecreasedEvent notification, CancellationToken cancellationToken)
        {
            var basketLine = notification.BasketLine;
            int oldQuantity = notification.OldQuantity;

            int gap = oldQuantity - basketLine.BasketItem.Quantity;

            string correlationId =_stockActionCorrelationIdGenerator.Generate(basketLine);
            AddToStockCommand addToStockCommand = new AddToStockCommand(basketLine.BasketItem.ProductId, gap, correlationId);
            await _domainMessageBroker.SendAsync(addToStockCommand, cancellationToken);
        }
    }
}