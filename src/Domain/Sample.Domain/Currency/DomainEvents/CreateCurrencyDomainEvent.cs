using Sample.SharedKernel.EventProcessing.DomainEvent;


namespace Sample.Domain.Currency.DomainEvents
{
    public class CreateCurrencyDomainEvent : DomainEventBase
    {
        public CreateCurrencyDomainEvent(string id) : base()
        {
            Id = id;
        }
        public string Id { get; set; }
    }
}
