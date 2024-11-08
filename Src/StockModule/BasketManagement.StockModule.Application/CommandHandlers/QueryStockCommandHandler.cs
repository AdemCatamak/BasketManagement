using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Domain.Pagination;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.SpecificationOperations;
using BasketManagement.Shared.Specification.ExpressionSpecificationSection.Specifications;
using BasketManagement.StockModule.Application.Commands;
using BasketManagement.StockModule.Domain;
using BasketManagement.StockModule.Domain.Repositories;
using BasketManagement.StockModule.Domain.Specifications.StockSpecifications;

namespace BasketManagement.StockModule.Application.CommandHandlers
{
    public class QueryStockCommandHandler : IDomainCommandHandler<QueryStockCommand, PaginatedCollection<StockResponse>>
    {
        private readonly IStockDbContext _stockDbContext;

        public QueryStockCommandHandler(IStockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }

        public async Task<PaginatedCollection<StockResponse>> Handle(QueryStockCommand request, CancellationToken cancellationToken)
        {
            IExpressionSpecification<Stock> specification = ExpressionSpecification<Stock>.Default;

            if (!string.IsNullOrEmpty(request.ProductId))
            {
                specification = specification.And(new ProductIdIs(request.ProductId));
            }

            if (request.InStock.HasValue)
            {
                if (request.InStock.Value)
                {
                    specification = specification.And(new StockCountGreaterThan(0));
                }
                else
                {
                    specification = specification.And(new StockCountLessThan(1));
                }
            }

            IStockRepository stockRepository = _stockDbContext.StockRepository;
            PaginatedCollection<Stock> paginatedCollection = await stockRepository.GetAsync(specification, request.Offset, request.Limit, cancellationToken);

            PaginatedCollection<StockResponse> result = new PaginatedCollection<StockResponse>(paginatedCollection.TotalCount,
                                                                                               paginatedCollection.Data
                                                                                                                  .Select(stock => new StockResponse(stock.ProductId,
                                                                                                                                                     stock.AvailableStock))
                                                                                                                  .ToList());
            return result;
        }
    }
}