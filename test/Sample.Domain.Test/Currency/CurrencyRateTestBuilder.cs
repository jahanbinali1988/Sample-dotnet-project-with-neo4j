using Moq;
using Sample.Domain.Currency;
using Sample.Domain.Currency.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Test.Currency
{
    internal class CurrencyRateTestBuilder
    {
        internal Mock<ICheckCurrencyRateDuplicationService> _currencyRateDuplicationService;
        internal Mock<ICheckCurrencyRateExistenceService> _currencyRateExistenceService;
        internal Guid _originCurrencyId;
        internal Guid _destinationCurrencyId;
        internal float _rate;
        public CurrencyRateTestBuilder()
        {

        }


        public CurrencyRateTestBuilder SetCurrencyRateDuplicationService(bool result)
        {
            _currencyRateDuplicationService.Setup(s => s.IsValidAsync(_originCurrencyId, _destinationCurrencyId)).ReturnsAsync(result);
            return this;
        }
        public CurrencyRateTestBuilder SetCurrencyRateExistenceService(bool result)
        {
            _currencyRateExistenceService.Setup(s => s.IsExistsAsync(_originCurrencyId, _destinationCurrencyId)).ReturnsAsync(result);
            return this;
        }

        public CurrencyRateTestBuilder WithOriginCurrenctId(Guid originCurrencyId)
        {
            this._originCurrencyId = originCurrencyId;

            return this;
        }
        public CurrencyRateTestBuilder WithDestinationId(Guid destinationCurrencyId)
        {
            this._destinationCurrencyId = destinationCurrencyId;

            return this;
        }
        public CurrencyRateTestBuilder WithRate(float rate)
        {
            this._rate = rate;

            return this;
        }

        public CurrencyRate Build()
        {
            return CurrencyRate.Create(_originCurrencyId, _destinationCurrencyId, _rate);
        }
    }
}
