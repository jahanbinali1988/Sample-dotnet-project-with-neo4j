using Sample.SharedKernel.EventProcessing.DomainEvent;

namespace Sample.Domain.Currency.DomainEvents
{
    public class AddRateDomainEvent : DomainEventBase
    {
        public AddRateDomainEvent(string originCurrencyId, string destinationCurrencyId, float rate) : base()
        {
            OriginCurrencyId = originCurrencyId;
            DestinationCurrencyId = destinationCurrencyId;
            Rate = rate;
        }

        public string OriginCurrencyId { get; }
        public string DestinationCurrencyId { get; }
        public float Rate { get; }
    }
}
