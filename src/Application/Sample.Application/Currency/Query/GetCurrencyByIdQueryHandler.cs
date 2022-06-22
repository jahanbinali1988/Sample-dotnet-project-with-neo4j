using Mapster;
using Sample.Application.Contract.Currency;
using Sample.Application.Contract.Currency.Query;
using Sample.Application.Contract.Exceptions;
using Sample.Domain.Currency;
using Sample.SharedKernel.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Application.Currency.Query
{
    public class GetCurrencyByIdQueryHandler : IQueryHandler<GetCurrencyByIdQuery, CurrencyResponseDto>
    {
        private readonly ICurrencyRepository _repository;
        public GetCurrencyByIdQueryHandler(ICurrencyRepository repository)
        {
            _repository = repository;
        }

        public async Task<CurrencyResponseDto> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
        {
            var currency = await _repository.GetAsync(request.CurrencyId, cancellationToken);
            if (currency is null)
                throw new EntityNotFoundException($"Unable to find currency with given id '{request.CurrencyId}'");

            return currency.Adapt<CurrencyResponseDto>();
        }
    }
}
