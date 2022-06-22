using System;
using System.Threading.Tasks;

namespace Sample.Domain.Currency.DomainServices
{
    public interface ICheckCurrencyNameExistenceService
    {
        Task<bool> IsValidAsync(Guid currencyId, string currencyName);
    }
}
