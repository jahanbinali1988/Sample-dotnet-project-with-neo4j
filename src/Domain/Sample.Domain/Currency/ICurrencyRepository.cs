using Sample.SharedKernel.Application;
using Sample.SharedKernel.Shared;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Domain.Currency
{
    public interface ICurrencyRepository : IRepository<CurrencyEntity>
    {
        void DetachEntity(CurrencyEntity meetingEntity);
        Task<IEnumerable<CurrencyEntity>> GetListByPaginationAsync(int count, int offset, CancellationToken token);
        Task<CurrencyRate> GetRateAsync(Guid originCurrencyId, Guid destinationCurrencyId, CancellationToken cancellationToken);
        Task<int> GetTotalCount(CancellationToken cancellationToken);
        Task<int> GetRateTotalCount(Guid currencyId, CancellationToken cancellationToken);
        Task<IEnumerable<CurrencyRate>> GetRateListByPaginationAsync(Guid currencyId, int count, int offset, CancellationToken cancellationToken);
        public Task<CurrencyEntity> GetWithRatesAsync(Guid id, CancellationToken cancellationToken);
    }
}
