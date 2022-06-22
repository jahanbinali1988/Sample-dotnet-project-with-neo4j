using Sample.Domain.Currency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Infrastructure.Domain.Currency
{
    internal class CurrencyRateTypeConfiguration : IEntityTypeConfiguration<CurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CurrencyRate> builder)
        {
            builder.ToTable("CurrencyRates");

            builder.HasKey("DestinationCurrencyId", "OriginCurrencyId");
        }
    }
}
