using System.Threading;
using System.Threading.Tasks;
using BasketManagement.BasketModule.Application.Commands;
using BasketManagement.BasketModule.Domain;
using BasketManagement.BasketModule.Domain.Repositories;
using BasketManagement.BasketModule.Domain.Specifications;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.SpecificationOperations;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace BasketManagement.BasketModule.Application.CommandHandlers
{
    public class DeleteBasketCommandHandler : IDomainCommandHandler<DeleteBasketCommand>
    {
        private readonly IBasketDbContext _basketDbContext;

        public DeleteBasketCommandHandler(IBasketDbContext basketDbContext)
        {
            _basketDbContext = basketDbContext;
        }

        public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            IExpressionSpecification<Basket> specification =
                new AccountIdIs(request.AccountId).And(new BasketIdIs(request.BasketId));

            var basketRepository = _basketDbContext.BasketRepository;
            var basket = await basketRepository.GetFirstAsync(specification, cancellationToken);
            await basketRepository.RemoveAsync(basket, cancellationToken);

            return true;
        }
    }
}