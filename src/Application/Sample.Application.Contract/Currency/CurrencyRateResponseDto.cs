using System;

namespace Sample.Application.Contract.Currency
{
    public class CurrencyRateResponseDto
    {
        public Guid DestinationCurrencyId { get; set; }
        public string DestinationCurrencyName { get; set; }
        public float Rate { get; set; }
    }
}
