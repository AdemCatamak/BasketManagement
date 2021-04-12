using System.Threading;
using System.Threading.Tasks;
using BasketManagement.BasketModule.Application.Commands;
using BasketManagement.BasketModule.Domain;
using BasketManagement.BasketModule.Domain.Repositories;
using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.BasketModule.Application.CommandHandlers
{
    public class CreateBasketCommandHandler : IDomainCommandHandler<CreateBasketCommand, BasketId>
    {
        private readonly IBasketDbContext _basketDbContext;

        public CreateBasketCommandHandler(IBasketDbContext basketDbContext)
        {
            _basketDbContext = basketDbContext;
        }

        public async Task<BasketId> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            IBasketRepository basketRepository = _basketDbContext.BasketRepository;

            Basket basket = Basket.Create(request.AccountId);
            await basketRepository.AddAsync(basket, cancellationToken);

            return basket.Id;
        }
    }
}