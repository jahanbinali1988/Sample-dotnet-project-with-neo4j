using System;

namespace Sample.Api.Models.Currency
{
    public class AddRateToCurrencyRequest
    {
        public Guid DestinationCurrencyId { get; set; }
        public float Rate { get; set; }
    }
}
