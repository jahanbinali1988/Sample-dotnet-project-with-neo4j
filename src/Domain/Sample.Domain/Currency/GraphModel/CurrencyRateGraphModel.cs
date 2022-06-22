using System;

namespace Sample.Domain.Currency.GraphModel
{
    public class CurrencyRateGraphModel
    {
        public string OriginCurrencyId { get; set; }
        public string DestinationCurrencyId { get; set; }
        public float Rate { get; set; }
    }
}
