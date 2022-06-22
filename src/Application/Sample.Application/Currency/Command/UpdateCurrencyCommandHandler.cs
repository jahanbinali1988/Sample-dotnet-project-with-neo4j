using MediatR;
using Sample.Application.Contract.Currency.Command;
using Sample.Application.Contract.Exceptions;
using Sample.Domain.Currency;
using Sample.Domain.Currency.DomainServices;
using Sample.SharedKernel.Application;
using Sample.SharedKernel.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Application.Currency.Command
{
    public class UpdateCurrencyCommandHandler : ICommandHandler<UpdateCurrencyCommand>
    {
        private readonly ICurrencyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICheckCurrencyNameExistenceService _currencyNameExistenceService;
        public UpdateCurrencyCommandHandler(ICurrencyRepository repository, IUnitOfWork unitOfWork, ICheckCurrencyNameExistenceService currencyNameExistenceService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _currencyNameExistenceService = currencyNameExistenceService;
        }

        public async Task<Unit> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency = await _repository.GetAsync(request.Id, cancellationToken);
            if (currency is null)
                throw new EntityNotFoundException($"Unable to find currency with given id '{request.Id}'");

            await currency.UpdateNameAsync(_currencyNameExistenceService, request.Name);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
