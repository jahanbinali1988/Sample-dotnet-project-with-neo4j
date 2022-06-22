using Mapster;
using Sample.Application.Contract.Currency;
using Sample.Application.Contract.Currency.Query;
using Sample.Domain.Currency;
using Sample.SharedKernel.Application;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Application.Currency.Query
{
    public class GetCurrencyRatesListQueryHandler : IQueryHandler<GetCurrencyRatesListQuery, Pagination<CurrencyRateResponseDto>>
    {
        private readonly ICurrencyRepository _repository;
        public GetCurrencyRatesListQueryHandler(ICurrencyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Pagination<CurrencyRateResponseDto>> Handle(GetCurrencyRatesListQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await _repository.GetRateTotalCount(request.CurrencyId, cancellationToken);
            var currencyRates = await _repository.GetRateListByPaginationAsync(request.CurrencyId, request.Count, request.Offset, cancellationToken);

            return new Pagination<CurrencyRateResponseDto>()
            {
                Items = currencyRates.Adapt<List<CurrencyRateResponseDto>>(),
                TotalItems = totalCount
            };
        }
    }
}
