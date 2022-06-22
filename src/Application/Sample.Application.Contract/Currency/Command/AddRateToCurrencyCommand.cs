using Sample.SharedKernel.Application;
using System;

namespace Sample.Application.Contract.Currency.Command
{
    public class AddRateToCurrencyCommand : CommandBase<CurrencyRateResponseDto>
    {
        public Guid CurrencyId { get; init; }
        public Guid DestinationCurrencyId { get; init; }
        public float Rate { get; init; }

        public AddRateToCurrencyCommand(Guid currencyId, Guid destinationCurrencyId, float rate)
        {
            this.CurrencyId = currencyId;
            this.DestinationCurrencyId = destinationCurrencyId;
            this.Rate = rate;
        }
    }
}
