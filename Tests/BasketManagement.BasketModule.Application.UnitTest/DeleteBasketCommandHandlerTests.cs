using System;
using System.Threading;
using System.Threading.Tasks;
using BasketManagement.BasketModule.Application.CommandHandlers;
using BasketManagement.BasketModule.Application.Commands;
using BasketManagement.BasketModule.Domain;
using BasketManagement.BasketModule.Domain.Exceptions;
using BasketManagement.BasketModule.Domain.Repositories;
using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;
using Moq;
using Xunit;

namespace BasketManagement.BasketModule.Application.UnitTest
{
    public class DeleteBasketCommandHandlerTests
    {
        private readonly Mock<IBasketDbContext> _basketDbContextMock;
        private readonly Mock<IBasketRepository> _basketRepositoryMock;

        private readonly DeleteBasketCommandHandler _sut;

        public DeleteBasketCommandHandlerTests()
        {
            _basketRepositoryMock = new Mock<IBasketRepository>();
            _basketDbContextMock = new Mock<IBasketDbContext>();
            _basketDbContextMock.Setup(context => context.BasketRepository)
                .Returns(_basketRepositoryMock.Object);

            _sut = new DeleteBasketCommandHandler(_basketDbContextMock.Object);
        }

        [Fact]
        public async Task DeleteBasketCommandHandler__WhenBasketNotExist__BasketNotFoundExceptionOccurs()
        {
            _basketRepositoryMock.Setup(repository => repository.GetFirstAsync(It.IsAny<IExpressionSpecification<Basket>>(), It.IsAny<CancellationToken>()))
                .Throws<BasketNotFoundException>();

            DeleteBasketCommand deleteBasketCommand = new DeleteBasketCommand("account-id", new BasketId(Guid.NewGuid()));
            await Assert.ThrowsAsync<BasketNotFoundException>(async () => await _sut.Handle(deleteBasketCommand, CancellationToken.None));

            _basketRepositoryMock.Verify(repository => repository.AddAsync(It.IsAny<Basket>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task DeleteBasketCommandHandler__WhenBasketExist__BasketLinesAndBasketShouldBeRemoved()
        {
            BasketItem basketItem = new BasketItem("productId", 1);
            Basket basket = Basket.Create("accountId");
            basket.PutItemIntoBasket(basketItem);
            _basketRepositoryMock.Setup(repository => repository.GetFirstAsync(It.IsAny<IExpressionSpecification<Basket>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(basket);
            _basketRepositoryMock.Setup(repository => repository.RemoveAsync(basket, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Callback(() =>
                {
                    basket.RemoveAllItemFromBasket();
                });

            DeleteBasketCommand deleteBasketCommand = new DeleteBasketCommand("account-id", new BasketId(Guid.NewGuid()));
            await _sut.Handle(deleteBasketCommand, CancellationToken.None);

            _basketRepositoryMock.Verify(repository => repository.RemoveAsync(basket, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Empty(basket.BasketLines);
        }
    }
}