using BasketManagement.BasketModule.Domain;

namespace BasketManagement.BasketModule.Application.Services
{
    public interface IStockActionCorrelationIdGenerator
    {
        string Generate(BasketLine basketLine);
    }
}