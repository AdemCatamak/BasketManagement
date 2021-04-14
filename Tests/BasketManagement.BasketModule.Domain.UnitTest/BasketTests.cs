using System;
using System.Linq;
using BasketManagement.BasketModule.Domain.Exceptions;
using BasketManagement.BasketModule.Domain.ValueObjects;
using Xunit;

namespace BasketManagement.BasketModule.Domain.UnitTest
{
    public class BasketTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CreateBasket__WhenAccountIdEmpty__AccountIdEmptyExceptionOccurs(string accountId)
        {
            Assert.Throws<AccountIdEmptyException>(() => Basket.Create(accountId));
        }

        [Fact]
        public void CreateBasket__RequestIsValid__EmptyBasketShouldBeCreated()
        {
            string accountId = Guid.NewGuid().ToString();

            var basket = Basket.Create(accountId);

            Assert.Equal(accountId, basket.AccountId);
            Assert.Empty(basket.BasketLines);
        }
        
        [Fact]
        public void PutItemIntoBasket__WhenQuantityIsZero__BasketShouldNotContainsProduct()
        {
            string accountId = Guid.NewGuid().ToString();
            BasketItem basketItem = new BasketItem("productId", 0);

            var basket = Basket.Create(accountId);
            basket.PutItemIntoBasket(basketItem);
            
            Assert.Empty(basket.BasketLines);
        }
        
        [Fact]
        public void PutItemIntoBasket__WhenProductExistInBasket_And_ProductQuantitySetAsZero__BasketShouldNotContainsProduct()
        {
            string accountId = Guid.NewGuid().ToString();
            BasketItem basketItem = new BasketItem("productId", 5);

            var basket = Basket.Create(accountId);
            basket.PutItemIntoBasket(basketItem);
            
            Assert.Contains(basketItem, basket.BasketLines.Select(l=>l.BasketItem));

            BasketItem newBasketItem = new BasketItem(basketItem.ProductId, 0);
            basket.PutItemIntoBasket(newBasketItem);
            
            Assert.Empty(basket.BasketLines);
        }
        
        [Fact]
        public void PutItemIntoBasket__WhenProductExistInBasket_And_ProductQuantitySetAsNonzero__BasketItemQuantityShouldBeUpdated()
        {
            string accountId = Guid.NewGuid().ToString();
            BasketItem basketItem = new BasketItem("productId", 5);

            var basket = Basket.Create(accountId);
            basket.PutItemIntoBasket(basketItem);
            
            Assert.Contains(basketItem, basket.BasketLines.Select(l=>l.BasketItem));

            BasketItem newBasketItem = new BasketItem(basketItem.ProductId, 3);
            basket.PutItemIntoBasket(newBasketItem);
            
            Assert.Equal(3, basket.BasketLines.First(l=>l.BasketItem.ProductId == "productId").BasketItem.Quantity);
        }
    }
}