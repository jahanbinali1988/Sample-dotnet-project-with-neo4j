using MediatR;
using Sample.Application.Contract.Currency.Command;
using Sample.Application.Contract.Exceptions;
using Sample.Domain.Currency;
using Sample.Domain.Currency.DomainServices;
using Sample.SharedKernel.Application;
using Sample.SharedKernel.SeedWork;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Application.Currency.Command
{
    public class DeleteCurrencyRateCommandHandler : ICommandHandler<DeleteCurrencyRateCommand>
    {
        private readonly ICurrencyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICheckCurrencyRateExistenceService _currencyRateExistenceService;
        public DeleteCurrencyRateCommandHandler(ICurrencyRepository repository, IUnitOfWork unitOfWork, ICheckCurrencyRateExistenceService currencyRateExistenceService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _currencyRateExistenceService = currencyRateExistenceService;
        }
        public async Task<Unit> Handle(DeleteCurrencyRateCommand request, CancellationToken cancellationToken)
        {
            var currency = await _repository.GetAsync(request.CurrencyId, cancellationToken);
            if (currency is null)
                throw new EntityNotFoundException($"Unable to find Origin Currency with given Id '{request.CurrencyId}'");

            var currencyRate = await _repository.GetRateAsync(request.CurrencyId, request.DestinationCurrencyId, cancellationToken);
            if (currencyRate is null)
                throw new EntityNotFoundException($"Unable to find Currency Rate with given Origin Id '{request.CurrencyId}' and Destination Id '{request.DestinationCurrencyId}'");

            await currency.DeleteCurrencyRateAsync(_currencyRateExistenceService, currencyRate);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
