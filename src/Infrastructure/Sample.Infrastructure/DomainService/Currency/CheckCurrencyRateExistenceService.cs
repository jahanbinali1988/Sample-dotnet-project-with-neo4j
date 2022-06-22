using Microsoft.EntityFrameworkCore;
using Sample.Domain.Currency.DomainServices;
using Sample.Infrastructure.Persistence;
using System;
using System.Threading.Tasks;

namespace Sample.Infrastructure.DomainService.Meeting
{
    public class CheckCurrencyRateExistenceService : ICheckCurrencyRateExistenceService
    {
        private readonly SampleDbContext _dbContext;
        public CheckCurrencyRateExistenceService(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsExistsAsync(Guid originCurrencyId, Guid destinationCurrencyId)
        {
            if (await _dbContext.CurrencyRates.AnyAsync(c => c.DestinationCurrencyId.Equals(destinationCurrencyId) && c.OriginCurrencyId.Equals(originCurrencyId)))
                return true;
            else
                return false;
        }
    }
}
