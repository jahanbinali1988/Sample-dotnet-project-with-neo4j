using Mapster;
using Sample.Application.Contract.Currency;
using Sample.Application.Contract.Currency.Mapping;
using Sample.Application.Contract.Currency.Query;
using Sample.Domain.Currency;
using Sample.SharedKernel.Application;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Application.Currency.Query
{
    public class GetCurrenciesListQueryHandler : IQueryHandler<GetCurrenciesListQuery, Pagination<CurrencyResponseDto>>
    {
        private readonly ICurrencyRepository _repository;
        public GetCurrenciesListQueryHandler(ICurrencyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Pagination<CurrencyResponseDto>> Handle(GetCurrenciesListQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await _repository.GetTotalCount(cancellationToken);
            var currencies = await _repository.GetListByPaginationAsync(request.Count, request.Offset, cancellationToken);

            return new Pagination<CurrencyResponseDto>()
            {
                Items = currencies.Map().ToList(),
                TotalItems = totalCount
            };
        }
    }
}
