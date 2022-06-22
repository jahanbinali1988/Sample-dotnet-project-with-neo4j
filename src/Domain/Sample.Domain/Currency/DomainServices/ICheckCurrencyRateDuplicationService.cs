using System;
using System.Threading.Tasks;

namespace Sample.Domain.Currency.DomainServices
{
    public interface ICheckCurrencyRateDuplicationService
    {
        Task<bool> IsValidAsync(Guid originCurrencyId, Guid destinationCurrencyId);
    }
}
