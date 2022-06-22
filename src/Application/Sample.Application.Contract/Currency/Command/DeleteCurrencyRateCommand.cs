using Sample.SharedKernel.Application;
using System;

namespace Sample.Application.Contract.Currency.Command
{
    public class DeleteCurrencyRateCommand : CommandBase
    {
        public Guid CurrencyId { get; init; }
        public Guid DestinationCurrencyId { get; init; }

        public DeleteCurrencyRateCommand(Guid currencyId, Guid destinationCurrencyId)
        {
            this.DestinationCurrencyId = destinationCurrencyId;
            CurrencyId = currencyId;
        }
    }
}
