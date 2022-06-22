using System;

namespace Sample.Api.Models.Currency
{
    public class ConvertAmountRequest
    {
        public Guid DestinationCurrencyId { get; set; }
        public int Amount { get; set; }
    }
}
