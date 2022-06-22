using Sample.SharedKernel.Application;
using System;

namespace Sample.Application.Contract.Currency.Query
{
    public class ConvertAmountQuery : IQuery<decimal>
    {
        public ConvertAmountQuery(Guid originCurrencyId, Guid destinationCurrencyId, decimal amount)
        {
            OriginCurrencyId = originCurrencyId;
            DestinationCurrencyId = destinationCurrencyId;
            Amount = amount;
        }
        public Guid OriginCurrencyId { get; init; }
        public Guid DestinationCurrencyId { get; init; }
        public decimal Amount { get; init; }
    }
}
