using Sample.Application.Contract.Exceptions;
using Sample.SharedKernel.Application;
using Sample.SharedKernel.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Sample.Application.Contract.Currency.Command;
using Sample.Domain.Currency;

namespace Sample.Application.Meeting.Command
{
    public class DeleteCurrencyCommandHandler : ICommandHandler<DeleteCurrencyCommand>
    {
        private readonly ICurrencyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCurrencyCommandHandler(ICurrencyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency = await _repository.GetAsync(request.CurrencyId, cancellationToken);
            if (currency is null)
                throw new EntityNotFoundException($"Unable to find currency with given id '{request.CurrencyId}'");

            await _repository.DeleteAsync(currency, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
