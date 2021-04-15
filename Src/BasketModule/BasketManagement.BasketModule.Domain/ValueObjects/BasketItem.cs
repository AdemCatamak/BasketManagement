using System.Collections.Generic;
using BasketManagement.BasketModule.Domain.Exceptions;
using BasketManagement.Shared.Domain;

namespace BasketManagement.BasketModule.Domain.ValueObjects
{
    public class BasketItem : ValueObject
    {
        public string ProductId { get; private set; }
        public int Quantity { get; private set; }

        public BasketItem(string productId, int quantity)
        {
            if (string.IsNullOrEmpty(productId)) throw new ProductIdEmptyException();
            if (quantity < 0) throw new NegativeQuantityException();
            ProductId = productId;
            Quantity = quantity;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ProductId;
            yield return Quantity;
        }
    }
}