using System.Threading.Tasks;

namespace Sample.SharedKernel.EventProcessing.DomainEvent
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
