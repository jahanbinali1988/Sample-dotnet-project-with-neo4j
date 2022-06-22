using Sample.Domain.Currency.DomainServices;
using Sample.Domain.Shared;
using Sample.SharedKernel.SeedWork;
using System;
using System.Threading.Tasks;

namespace Sample.Domain.Currency.Rules
{
    public class CurrencyRateExistenceRule : IBusinessRule
    {
        private readonly ICheckCurrencyRateExistenceService _currencyRateExistenceService;
        private readonly Guid _originCurrencyId;
        private readonly Guid _destinationCurrencyId;

        public CurrencyRateExistenceRule(ICheckCurrencyRateExistenceService currencyRateExistenceService, Guid originCurrencyId, Guid destinationCurrencyId)
        {
            _currencyRateExistenceService = currencyRateExistenceService;
            _originCurrencyId = originCurrencyId;
            _destinationCurrencyId = destinationCurrencyId;
        }

        public string Message => $"Unable to find currency rate for currency with geiven id '{_destinationCurrencyId}' from '{_originCurrencyId}' f";

        public string[] Properties => new[] { nameof(CurrencyEntity.CurrencyRates) };

        public string ErrorType => BusinessRuleType.ValueConstraints.ToString("G");

        public async Task<bool> IsBroken() => !await _currencyRateExistenceService.IsExistsAsync(_originCurrencyId, _destinationCurrencyId);
    }
}
