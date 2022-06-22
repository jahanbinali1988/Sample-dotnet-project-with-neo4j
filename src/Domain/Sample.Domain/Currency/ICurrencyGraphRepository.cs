using Sample.Domain.Currency.GraphModel;
using System.Threading.Tasks;

namespace Sample.Domain.Currency
{
    public interface ICurrencyGraphRepository
    {
        Task CreateAsync(CurrencyGraphModel currency);
        Task AddRateAsync(CurrencyRateGraphModel currencyRate);
        Task<CurrencyDestinationRoute> GetRouteAsync(CurrencyGraphModel originCurrency, CurrencyGraphModel destinationCurrency);
    }
}
