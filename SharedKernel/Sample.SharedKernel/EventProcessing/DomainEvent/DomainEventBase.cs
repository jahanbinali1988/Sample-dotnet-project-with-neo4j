using Sample.SharedKernel.SeedWork;
using System;

namespace Sample.SharedKernel.EventProcessing.DomainEvent
{
    [Serializable]
    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase()
        {
            this.OccurredOn = DateTime.Now;
        }

        public DateTimeOffset OccurredOn { get; }
    }
}
