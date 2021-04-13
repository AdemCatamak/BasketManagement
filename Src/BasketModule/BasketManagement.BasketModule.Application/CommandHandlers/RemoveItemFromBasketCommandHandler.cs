using System.Threading;
using System.Threading.Tasks;
using BasketManagement.BasketModule.Application.Commands;
using BasketManagement.BasketModule.Domain;
using BasketManagement.BasketModule.Domain.Repositories;
using BasketManagement.BasketModule.Domain.Specifications;
using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.SpecificationOperations;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.BasketModule.Application.CommandHandlers
{
    public class RemoveItemFromBasketCommandHandler : IDomainCommandHandler<RemoveItemFromBasket>
    {
        private readonly IBasketDbContext _basketDbContext;

        public RemoveItemFromBasketCommandHandler(IBasketDbContext basketDbContext)
        {
            _basketDbContext = basketDbContext;
        }

        public async Task<bool> Handle(RemoveItemFromBasket request, CancellationToken cancellationToken)
        {
            IExpressionSpecification<Basket> specification =
                new AccountIdIs(request.AccountId).And(new BasketIdIs(request.BasketId));

            var basketRepository = _basketDbContext.BasketRepository;
            var basket = await basketRepository.GetFirstAsync(specification, cancellationToken);
            
            basket.RemoveItemFromBasket(request.ProductId);
            return true;
        }
    }
}