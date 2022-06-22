using Microsoft.EntityFrameworkCore;
using Sample.Domain.Currency.DomainServices;
using Sample.Infrastructure.Persistence;
using System;
using System.Threading.Tasks;

namespace Sample.Infrastructure.DomainService.Currency
{
    public class CheckCurrencyNameExistenceService : ICheckCurrencyNameExistenceService
    {
        private readonly SampleDbContext _dbContext;
        public CheckCurrencyNameExistenceService(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsValidAsync(Guid currencyId, string name)
        {
            if (currencyId.Equals(default) || ! await _dbContext.Currencies.AnyAsync(c => c.Id.Equals(currencyId) && c.Name.Equals(name)))
                return true;
            else 
                return false;
        }
    }
}
