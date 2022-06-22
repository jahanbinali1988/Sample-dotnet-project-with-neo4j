using Sample.SharedKernel.Application;
using System;

namespace Sample.Application.Contract.Currency.Query
{
    public class GetCurrencyByIdQuery : IQuery<CurrencyResponseDto>
    {
        public Guid CurrencyId { get; init; }

        public GetCurrencyByIdQuery(Guid currencyId)
        {
            this.CurrencyId = currencyId;
        }
    }
}
