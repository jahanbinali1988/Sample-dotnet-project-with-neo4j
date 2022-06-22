using Sample.Domain.Currency.DomainServices;
using Sample.Domain.Shared;
using Sample.SharedKernel.SeedWork;
using System;
using System.Threading.Tasks;

namespace Sample.Domain.Currency.Rules
{
    public class CurrencyNameExistenceRule : IBusinessRule
    {
        private readonly ICheckCurrencyNameExistenceService _currencyNameExistenceService;
        private readonly Guid _currencyId;
        private readonly string _currencyName;
        public CurrencyNameExistenceRule(ICheckCurrencyNameExistenceService currencyNameExistenceService, Guid currencyId, string currencyName)
        {
            _currencyNameExistenceService = currencyNameExistenceService;
            _currencyId = currencyId;
            _currencyName = currencyName;
        }

        public string Message => $"Unable to save currency with given id '{_currencyId}' and name '{_currencyName}'";

        public string[] Properties => new[] { nameof(CurrencyEntity.Name) };

        public string ErrorType => BusinessRuleType.IdValidity.ToString("G");

        public async Task<bool> IsBroken() => !await _currencyNameExistenceService.IsValidAsync(_currencyId, _currencyName);
    }
}
