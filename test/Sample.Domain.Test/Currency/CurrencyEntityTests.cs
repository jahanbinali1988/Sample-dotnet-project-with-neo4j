using Sample.Domain.Currency.GraphModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sample.Domain.Test.Currency
{
    public class CurrencyEntityTests
    {
        public CurrencyEntityTests()
        {

        }

        [Fact]
        internal async Task Create_currency_entity_successfully()
        {
            var builder = new CurrencyEntityTestBuilder();
            var currency = await builder.BuildAsync();

            Assert.NotNull(currency);
            Assert.NotEqual(Guid.Empty, currency.Id);

        }

        [Theory]
        [ClassData(typeof(SampleData))]
        internal async Task ConvertAmount(CurrencyDestinationRoute currencyDestinationRoute, int actualAmount, decimal expectedAmount)
        {
            var currency = await (new CurrencyEntityTestBuilder()).BuildAsync();

            Assert.Equal(expectedAmount, currency.ConvertAmount(currencyDestinationRoute, actualAmount));
        }

        private class SampleData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                var route = new CurrencyDestinationRoute() { costs = new float[] { } };
                yield return new object[]
                {
                     route, 100, 100
                };

                var route2 = new CurrencyDestinationRoute() { costs = new float[] { 0.1F } };
                yield return new object[]
                {
                     route2, 100, 10
                };
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
    }
}
