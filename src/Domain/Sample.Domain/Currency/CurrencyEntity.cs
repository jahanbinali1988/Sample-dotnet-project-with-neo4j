using Ardalis.GuardClauses;
using Sample.Domain.Currency.DomainEvents;
using Sample.Domain.Currency.DomainServices;
using Sample.Domain.Currency.GraphModel;
using Sample.Domain.Currency.Rules;
using Sample.SharedKernel.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Domain.Currency
{
    public class CurrencyEntity : Entity, IAggregateRoot
    {
        private CurrencyEntity()
        {
            CurrencyRates = new List<CurrencyRate>();
        }
        private CurrencyEntity(string name, Guid? id)
        {
            CurrencyRates = new List<CurrencyRate>();
            if (!id.HasValue)
                id = Guid.NewGuid();

            base.Id = id.Value;
        }

        public static async Task<CurrencyEntity> CreateAsync(ICheckCurrencyNameExistenceService currencyNameExistenceService, string name, Guid? id)
        {
            var meeting = new CurrencyEntity(name, id);

            await meeting.UpdateNameAsync(currencyNameExistenceService, name);
            meeting.AddDomainEvent(new CreateCurrencyDomainEvent(meeting.Id.ToString()));

            return meeting;
        }

        public async Task UpdateNameAsync(ICheckCurrencyNameExistenceService currencyNameExistenceService, string currencyName)
        {
            await CheckRule(new CurrencyNameExistenceRule(currencyNameExistenceService, Id, currencyName));

            UpdateName(currencyName);
        }

        public async Task<CurrencyRate> AddCurrencyRateAsync(ICheckCurrencyRateDuplicationService currencyRateDuplicationService, Guid toCurrencyId, float rate)
        {
            await CheckRule(new CurrencyRateDuplicationRule(currencyRateDuplicationService, Id, toCurrencyId));

            var currencyRate = CurrencyRate.Create(Id, toCurrencyId, rate);
            CurrencyRates.Add(currencyRate);

            AddDomainEvent(new AddRateDomainEvent(Id.ToString(), toCurrencyId.ToString(), rate));

            return currencyRate;
        }

        public async Task DeleteCurrencyRateAsync(ICheckCurrencyRateExistenceService currencyRateExistenceService, CurrencyRate currencyRate)
        {
            await CheckRule(new CurrencyRateExistenceRule(currencyRateExistenceService, currencyRate.OriginCurrencyId, currencyRate.DestinationCurrencyId));

            CurrencyRates.Remove(currencyRate);
        }

        public decimal ConvertAmount(CurrencyDestinationRoute currencyDestinationRoute, decimal amount)
        {
            decimal convertedAmount = amount;

            foreach (var cost in currencyDestinationRoute.costs)
            {
                convertedAmount = convertedAmount * (decimal)cost;
            }

            return convertedAmount;
        }

        private void UpdateName(string name)
        {
            Guard.Against.Default(name, nameof(name));
            this.Name = name;
        }
        public string Name { get; private set; }
        public List<CurrencyRate> CurrencyRates { get; set; }
    }
}
