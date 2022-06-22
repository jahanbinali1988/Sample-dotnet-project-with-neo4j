using Sample.Domain.Currency.DomainServices;
using Sample.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Sample.Infrastructure.DomainService.Currency
{
    public class CheckCurrencyRateDuplicationService : ICheckCurrencyRateDuplicationService
    {
        private readonly SampleDbContext _dbContext;
        public CheckCurrencyRateDuplicationService(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsValidAsync(Guid currencyId, Guid toCurrencyId)
        {
            var currencyRates = await _dbContext.CurrencyRates.FirstOrDefaultAsync(c=> c.OriginCurrencyId.Equals(currencyId) && c.DestinationCurrencyId.Equals(toCurrencyId));
            return currencyRates == null;
        }
    }
}
