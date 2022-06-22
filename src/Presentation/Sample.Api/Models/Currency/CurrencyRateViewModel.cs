using System;

namespace Sample.Api.Models.Currency
{
    public class CurrencyRateViewModel : ViewModelBase
    {
        public int Rate { get; set; }
        public Guid DestinationCurrencyId { get; set; }
        public string DestinationCurrencyName { get; set; }
    }
}
