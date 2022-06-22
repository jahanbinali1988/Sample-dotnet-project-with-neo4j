using Microsoft.Extensions.Logging;
using Sample.Domain.Currency;
using Sample.Domain.Currency.DomainEvents;
using Sample.Domain.Currency.GraphModel;
using Sample.SharedKernel.EventProcessing.DomainEvent;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Application.Currency.DomainEventConsumer
{
    public class CreateCurrencyDomainEventConsumer : DomainEventHandler<CreateCurrencyDomainEvent>
    {
        private readonly ICurrencyGraphRepository _repository;
        public CreateCurrencyDomainEventConsumer(ILogger<CreateCurrencyDomainEventConsumer> logger, ICurrencyGraphRepository repository) : base(logger)
        {
            _repository = repository;
        }

        protected override async Task HandleEvent(CreateCurrencyDomainEvent notification, CancellationToken cancellationToken)
        {
            var model = new CurrencyGraphModel()
            {
                Id = notification.Id
            };
            await _repository.CreateAsync(model);
        }
    }
}
