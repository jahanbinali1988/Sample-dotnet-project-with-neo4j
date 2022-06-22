using Sample.Domain.Currency;
using Sample.Infrastructure.Domain.Currency;
using Microsoft.EntityFrameworkCore;

namespace Sample.Infrastructure.Persistence
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CurrencyTypeConfiguration());
            builder.ApplyConfiguration(new CurrencyRateTypeConfiguration());
        }

        public DbSet<CurrencyEntity> Currencies { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

    }
}
