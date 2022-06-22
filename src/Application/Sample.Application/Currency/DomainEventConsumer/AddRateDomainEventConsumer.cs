using Microsoft.Extensions.Logging;
using Sample.Domain.Currency;
using Sample.Domain.Currency.DomainEvents;
using Sample.Domain.Currency.GraphModel;
using Sample.SharedKernel.EventProcessing.DomainEvent;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Application.Currency.DomainEventConsumer
{
    public class AddRateDomainEventConsumer : DomainEventHandler<AddRateDomainEvent>
    {
        private readonly ICurrencyGraphRepository _repository;
        public AddRateDomainEventConsumer(ILogger<AddRateDomainEventConsumer> logger, ICurrencyGraphRepository repository) : base(logger)
        {
            _repository = repository;
        }

        protected override async Task HandleEvent(AddRateDomainEvent notification, CancellationToken cancellationToken)
        {
            var model = new CurrencyRateGraphModel()
            {
                DestinationCurrencyId = notification.DestinationCurrencyId,
                OriginCurrencyId = notification.OriginCurrencyId,
                Rate = notification.Rate
            };
            await _repository.AddRateAsync(model);
        }
    }
}
