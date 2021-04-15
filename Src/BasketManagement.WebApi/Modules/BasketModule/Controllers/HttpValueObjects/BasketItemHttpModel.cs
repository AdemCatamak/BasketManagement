namespace BasketManagement.WebApi.Modules.BasketModule.Controllers.HttpValueObjects
{
    public class BasketItemHttpModel
    {
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
    }
}