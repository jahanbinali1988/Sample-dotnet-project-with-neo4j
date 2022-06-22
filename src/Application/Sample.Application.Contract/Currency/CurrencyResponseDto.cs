using Sample.Application.Contract.Shared;
using System.Collections.Generic;

namespace Sample.Application.Contract.Currency
{
    public class CurrencyResponseDto : EntityBaseDto
    {
        public string Name { get; set; }
        public IEnumerable<CurrencyRateResponseDto> CurrencyRates { get; set; }
    }
}
