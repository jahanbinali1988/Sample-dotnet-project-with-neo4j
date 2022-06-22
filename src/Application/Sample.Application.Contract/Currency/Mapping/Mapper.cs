using Sample.Domain.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Application.Contract.Currency.Mapping
{
    public static class Mapper
    {
        public static IEnumerable<CurrencyResponseDto> Map(this IEnumerable<CurrencyEntity> currencies)
        {
            var result = new List<CurrencyResponseDto>();
            foreach (var currency in currencies)
            {
                result.Add(currency.Map());
            }
            return result;
        }

        public static CurrencyResponseDto Map(this CurrencyEntity currency)
        {
            return new CurrencyResponseDto()
            {
                Id = currency.Id,
                Name = currency.Name,
                CurrencyRates = currency.CurrencyRates.Map()
            };
        }

        public static IEnumerable<CurrencyRateResponseDto> Map(this IEnumerable<CurrencyRate> rates)
        {
            var result = new List<CurrencyRateResponseDto>();
            foreach (var rate in rates)
            {
                result.Add(rate.Map());
            }
            return result;
        }

        public static CurrencyRateResponseDto Map(this CurrencyRate rate)
        {
            return new CurrencyRateResponseDto()
            {
                DestinationCurrencyId = rate.DestinationCurrencyId,
                Rate = rate.Rate,
            };
        }
    }
}
