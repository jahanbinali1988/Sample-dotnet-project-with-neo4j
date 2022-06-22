using Sample.Domain.Currency;
using Sample.Infrastructure.Persistence;
using Sample.SharedKernel.Application;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Sample.Infrastructure.Domain.Currency
{
    public class CurrencyRepository : RepositoryBase<CurrencyEntity>, ICurrencyRepository
    {
        public CurrencyRepository(SampleDbContext dbContext) : base(dbContext)
        {
        }

        public void DetachEntity(CurrencyEntity meetingEntity)
        {
            DbContext.Entry(meetingEntity).State = EntityState.Detached;
        }

        public async Task<IEnumerable<CurrencyEntity>> GetListByPaginationAsync(int count, int offset, CancellationToken token)
        {
            return await base.DbContext.Currencies.Skip(count * offset).Take(count).ToListAsync();
        }

        public async Task<int> GetTotalCount(CancellationToken cancellationToken)
        {
            var count = await base.DbContext.CurrencyRates.CountAsync();
            return count;
        }

        public async Task<int> GetRateTotalCount(Guid currencyId, CancellationToken cancellationToken)
        {
            var count = await base.DbContext.CurrencyRates.CountAsync(c => c.OriginCurrencyId.Equals(currencyId));
            return count;
        }

        public async Task<IEnumerable<CurrencyRate>> GetRateListByPaginationAsync(Guid currencyId, int count, int offset, CancellationToken cancellationToken)
        {
            return await base.DbContext.CurrencyRates.Where(c => c.OriginCurrencyId.Equals(currencyId)).Skip(count * offset).Take(count).ToListAsync();
        }

        public Task<CurrencyRate> GetRateAsync(Guid originCurrencyId, Guid destinationCurrencyId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CurrencyEntity> GetWithRatesAsync(Guid id, CancellationToken cancellationToken)
        {
            return DbContext.Set<CurrencyEntity>()
                .AsTracking()
                .Include(c=> c.CurrencyRates)
                .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }
    }
}
