using Sample.Application.Contract.Currency;
using Sample.Application.Contract.Currency.Command;
using Sample.Domain.Currency;
using Sample.SharedKernel.Application;
using Sample.SharedKernel.SeedWork;
using Mapster;
using System.Threading;
using System.Threading.Tasks;
using Sample.Domain.Currency.DomainServices;

namespace Sample.Application.Currency.Command
{
    internal class CreateCurrencyCommandHandler : ICommandHandler<CreateCurrencyCommand, CurrencyResponseDto>
    {
        private readonly ICheckCurrencyNameExistenceService _currencyNameExistenceService;
        private readonly ICurrencyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCurrencyCommandHandler(ICurrencyRepository repository, IUnitOfWork unitOfWork, ICheckCurrencyNameExistenceService currencyNameExistenceService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _currencyNameExistenceService = currencyNameExistenceService;
        }

        public async Task<CurrencyResponseDto> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency = await CurrencyEntity.CreateAsync(_currencyNameExistenceService, request.Name, null);

            await _repository.AddAsync(currency, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _repository.DetachEntity(currency);

            return currency.Adapt<CurrencyResponseDto>();
        }
    }
}
