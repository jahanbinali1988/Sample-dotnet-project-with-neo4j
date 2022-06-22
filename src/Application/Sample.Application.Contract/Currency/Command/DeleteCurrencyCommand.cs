using Sample.SharedKernel.Application;
using System;

namespace Sample.Application.Contract.Currency.Command
{
    public class DeleteCurrencyCommand : CommandBase
    {
        public Guid CurrencyId { get; init; }

        public DeleteCurrencyCommand(Guid currencyId)
        {
            this.CurrencyId = currencyId;
        }
    }
}
