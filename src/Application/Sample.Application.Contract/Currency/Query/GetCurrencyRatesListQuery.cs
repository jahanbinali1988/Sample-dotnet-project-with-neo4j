using Sample.Application.Contract.Shared;
using Sample.SharedKernel.Application;
using System;

namespace Sample.Application.Contract.Currency.Query
{
    public class GetCurrencyRatesListQuery : GetListQueryBase, IQuery<Pagination<CurrencyRateResponseDto>>
    {
        public GetCurrencyRatesListQuery(Guid currencyId,int offset, int count) : base(offset, count)
        {
            CurrencyId = currencyId;
        }

        public Guid CurrencyId { get; init; }
    }
}

