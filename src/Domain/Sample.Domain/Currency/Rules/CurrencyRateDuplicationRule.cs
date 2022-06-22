using Sample.Domain.Currency.DomainServices;
using Sample.Domain.Shared;
using Sample.SharedKernel.SeedWork;
using System;
using System.Threading.Tasks;

namespace Sample.Domain.Currency.Rules
{
    public class CurrencyRateDuplicationRule : IBusinessRule
    {
        private readonly ICheckCurrencyRateDuplicationService _currencyRateDuplicationService;
        public Guid _originCurrencyId { get; }
        public Guid _destinationCurrencyId { get; }
        public CurrencyRateDuplicationRule(ICheckCurrencyRateDuplicationService currencyRateDuplicationService, Guid originCurrencyId, Guid destinationCurrencyId)
        {
            _currencyRateDuplicationService = currencyRateDuplicationService;
            _originCurrencyId = originCurrencyId;
            _destinationCurrencyId = destinationCurrencyId;
        }

        public string Message => $"The given CurrencyId '{_destinationCurrencyId}' is already exists for currency with id '{_originCurrencyId}'";

        public string[] Properties => new[] { nameof(CurrencyEntity.CurrencyRates) };

        public string ErrorType => BusinessRuleType.PropertyValue.ToString("G");

        public async Task<bool> IsBroken() => !await _currencyRateDuplicationService.IsValidAsync(_originCurrencyId, _destinationCurrencyId);
    }
}
