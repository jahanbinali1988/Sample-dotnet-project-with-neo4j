using Sample.SharedKernel.Application;
using System.Threading;
using System.Threading.Tasks;
using Sample.Domain.Currency;
using Sample.Application.Contract.Currency.Query;
using Sample.Application.Contract.Exceptions;
using Sample.Domain.Currency.GraphModel;
using System;

namespace Sample.Application.Meeting.Query
{
    public class ConvertAmountQueryHandler : IQueryHandler<ConvertAmountQuery, decimal>
    {
        private readonly ICurrencyRepository _repository;
        private readonly ICurrencyGraphRepository _graphRepository;
        public ConvertAmountQueryHandler(ICurrencyRepository repository, ICurrencyGraphRepository graphRepository)
        {
            _repository = repository;
            _graphRepository = graphRepository;
        }

        public async Task<decimal> Handle(ConvertAmountQuery request, CancellationToken cancellationToken)
        {
            var currency = await _repository.GetWithRatesAsync(request.OriginCurrencyId, cancellationToken);
            if (currency is null)
                throw new EntityNotFoundException($"Unable to find origin currency with given id '{request.OriginCurrencyId}'");

            var destinationCurrency = await _repository.GetWithRatesAsync(request.DestinationCurrencyId, cancellationToken);
            if (destinationCurrency is null)
                throw new EntityNotFoundException($"Unable to find destination currency with given id '{request.DestinationCurrencyId}'");

            var originGraph = new CurrencyGraphModel() { Id = request.OriginCurrencyId.ToString() };
            var destinationGraph = new CurrencyGraphModel() { Id = request.DestinationCurrencyId.ToString() };
            var route = await _graphRepository.GetRouteAsync(originGraph, destinationGraph);

            if (route is null || route.costs.Length == 0)
                throw new ArgumentException($"There is no way to convert amount from Currenty with geiven value {request.OriginCurrencyId} to currency with given id {request.DestinationCurrencyId}");

            decimal convertedAmount =  currency.ConvertAmount(route, request.Amount);

            return convertedAmount;
        }
    }
}
