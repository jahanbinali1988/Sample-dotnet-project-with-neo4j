using Sample.Domain.Currency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Sample.Infrastructure.Domain.Currency
{
    internal class CurrencyTypeConfiguration : IEntityTypeConfiguration<CurrencyEntity>
    {
        public void Configure(EntityTypeBuilder<CurrencyEntity> builder)
        {
            builder.ToTable("Currencies");

            builder.HasIndex(x => x.Id)
                .IsUnique();

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.Property<bool>("IsDeleted")
                .HasDefaultValue(false);

            builder.Property<DateTimeOffset?>("DeletedAt")
                .IsRequired(false);

            builder.HasMany<CurrencyRate>(c => c.CurrencyRates);

            builder.HasQueryFilter(p => EF.Property<bool>(p, "IsDeleted") == false);

        }
    }
}
