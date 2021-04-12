using BasketManagement.BasketModule.Domain;

namespace BasketManagement.BasketModule.Application.Services
{
    public class StockActionCorrelationIdGenerator : IStockActionCorrelationIdGenerator
    {
        public string Generate(BasketLine basketLine)
        {
            return $"{basketLine.Id}--{basketLine.BasketItem.ProductId}";
        }
    }
}