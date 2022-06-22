using System;
using System.Threading.Tasks;

namespace Sample.Domain.Currency.DomainServices
{
    public interface ICheckCurrencyRateExistenceService
    {
        Task<bool> IsExistsAsync(Guid originCurrencyId, Guid destinationCurrencyId);
    }
}
