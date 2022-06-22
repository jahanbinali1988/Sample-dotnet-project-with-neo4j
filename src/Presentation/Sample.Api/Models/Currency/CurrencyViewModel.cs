using System.Collections.Generic;

namespace Sample.Api.Models.Currency
{
    public class CurrencyViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public IEnumerable<CurrencyRateViewModel> CurrencyRates { get; set; }
    }
}
