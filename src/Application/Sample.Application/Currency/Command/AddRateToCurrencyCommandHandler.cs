using Sample.Application.Contract.Exceptions;
using Sample.SharedKernel.Application;
using Sample.SharedKernel.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Sample.Application.Contract.Currency.Command;
using Sample.Domain.Currency;
using Sample.Domain.Currency.DomainServices;
using Sample.Application.Contract.Currency;
using Mapster;

namespace Sample.Application.Meeting.Command
{
    public class AddRateToCurrencyCommandHandler : ICommandHandler<AddRateToCurrencyCommand, CurrencyRateResponseDto>
    {
        private readonly ICheckCurrencyRateDuplicationService _currencyRateDuplicationService;
        private readonly ICurrencyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public AddRateToCurrencyCommandHandler(ICurrencyRepository repository, IUnitOfWork unitOfWork, ICheckCurrencyRateDuplicationService currencyRateDuplicationService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _currencyRateDuplicationService = currencyRateDuplicationService;
        }

        public async Task<CurrencyRateResponseDto> Handle(AddRateToCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency = await _repository.GetAsync(request.CurrencyId, cancellationToken);
            if (currency == null)
                throw new EntityNotFoundException($"Unable to find currency with given id '{request.CommandId}'");

            var currencyRate = await currency.AddCurrencyRateAsync(_currencyRateDuplicationService, request.DestinationCurrencyId, request.Rate);
            await _repository.UpdateAsync(currency, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return currencyRate.Adapt<CurrencyRateResponseDto>();
        }
    }
}
