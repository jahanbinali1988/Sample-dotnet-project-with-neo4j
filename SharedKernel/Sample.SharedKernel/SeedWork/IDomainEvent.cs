using System;
using MediatR;

namespace Sample.SharedKernel.SeedWork
{
    public interface IDomainEvent : INotification
    {
        DateTimeOffset OccurredOn { get; }
    }
}
