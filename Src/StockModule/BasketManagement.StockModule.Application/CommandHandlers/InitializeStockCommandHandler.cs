using System;
using System.Threading;
using System.Threading.Tasks;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.StockModule.Application.Commands;
using BasketManagement.StockModule.Domain;
using BasketManagement.StockModule.Domain.Repositories;
using BasketManagement.StockModule.Domain.Rules;

namespace BasketManagement.StockModule.Application.CommandHandlers
{
    public class InitializeStockCommandHandler : IDomainCommandHandler<InitializeStockCommand, Guid>
    {
        private readonly IStockDbContext _stockDbContext;
        private readonly IStockActionUniqueChecker _stockActionUniqueChecker;

        public InitializeStockCommandHandler(IStockDbContext stockDbContext, IStockActionUniqueChecker stockActionUniqueChecker)
        {
            _stockDbContext = stockDbContext;
            _stockActionUniqueChecker = stockActionUniqueChecker;
        }

        public async Task<Guid> Handle(InitializeStockCommand request, CancellationToken cancellationToken)
        {
            StockAction stockAction = StockAction.Create(request.ProductId, StockActionTypes.InitializeStock, request.AvailableStock, request.CorrelationId, _stockActionUniqueChecker, cancellationToken);

            IStockActionRepository stockActionRepository = _stockDbContext.StockActionRepository;
            await stockActionRepository.AddAsync(stockAction, cancellationToken);

            return stockAction.Id;
        }
    }
}