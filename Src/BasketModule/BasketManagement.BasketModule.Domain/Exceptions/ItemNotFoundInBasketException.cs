using System;
using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.BasketModule.Domain.Exceptions
{
    public class ItemNotFoundInBasketException : NotFoundException
    {
        public ItemNotFoundInBasketException(string productId) :
            this($"{productId} does not exist in basket", null)
        {
        }

        public ItemNotFoundInBasketException(string message, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}