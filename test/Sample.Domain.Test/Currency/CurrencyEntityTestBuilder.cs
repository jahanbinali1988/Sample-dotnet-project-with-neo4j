using Sample.Domain.Currency;
using Sample.Domain.Currency.DomainServices;
using Moq;
using System;
using System.Threading.Tasks;

namespace Sample.Domain.Test.Currency
{
    public class CurrencyEntityTestBuilder
    {
        internal Guid _id;
        internal string _name;
        internal Mock<ICheckCurrencyNameExistenceService> _currencyNameExistenceService;

        public CurrencyEntityTestBuilder()
        {
            _currencyNameExistenceService = new Mock<ICheckCurrencyNameExistenceService>();

            WithId(Guid.NewGuid());
            WithName(Guid.NewGuid().ToString());
            SetCurrencyNameExistenceService(true);
        }

        public CurrencyEntityTestBuilder SetCurrencyNameExistenceService(bool result)
        {
            _currencyNameExistenceService.Setup(s => s.IsValidAsync(_id, _name)).ReturnsAsync(result);
            return this;
        }

        public CurrencyEntityTestBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }
        public CurrencyEntityTestBuilder WithName(string name) 
        {
            _name = name;
            return this;
        }
        public async Task<CurrencyEntity> BuildAsync()
        {
            return await CurrencyEntity.CreateAsync(_currencyNameExistenceService.Object, _name, _id);
        }
    }
}
