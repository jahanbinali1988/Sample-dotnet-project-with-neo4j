using Sample.Application.Contract.Shared;
using Sample.SharedKernel.Application;

namespace Sample.Application.Contract.Currency.Query
{
    public class GetCurrenciesListQuery : GetListQueryBase, IQuery<Pagination<CurrencyResponseDto>>
    {
        public GetCurrenciesListQuery(int offset, int count) : base(offset, count)
        {
        }
    }
}