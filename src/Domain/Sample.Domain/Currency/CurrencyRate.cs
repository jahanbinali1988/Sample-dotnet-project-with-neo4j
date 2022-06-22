using Sample.SharedKernel.SeedWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Domain.Currency
{
    public class CurrencyRate : ValueObject
    {
        public Guid OriginCurrencyId { get; set; }
        public Guid DestinationCurrencyId { get; set; }
        public float Rate { get; set; }

        public CurrencyRate(Guid originCurrencyId, Guid destinationCurrencyId, float rate)
        {
            OriginCurrencyId = originCurrencyId;
            DestinationCurrencyId = destinationCurrencyId;
            Rate = rate;
        }

        public static CurrencyRate Create(Guid originCurrencyId, Guid destinationCurrencyId, float rate)
        {
            var currencyRate = new CurrencyRate(originCurrencyId, destinationCurrencyId, rate);

            return currencyRate;
        }

        internal async Task<IEnumerable<CurrencyRate>> GetSubCurrencyRatesAsync(ICurrencyRepository repository, CancellationToken token)
        {
            var currency = await repository.GetWithRatesAsync(DestinationCurrencyId, token);
            return currency.CurrencyRates;
        }
    }
}
