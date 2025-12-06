using CastlePlus2.Domain.Entities.Rdzen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Rdzen
{
    public class EncjaConfiguration : IEntityTypeConfiguration<Encja>
    {
        public void Configure(EntityTypeBuilder<Encja> builder)
        {
            // Mapowanie tabeli bazowej
            builder.ToTable("Encja", "rdzen");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("IdEncji").HasDefaultValueSql("newsequentialid()");

            builder.Property(e => e.TypEncji).IsRequired().HasMaxLength(40);
            builder.Property(e => e.KodEncji).HasMaxLength(40);

            builder.Property(e => e.UtworzonoUtc)
                .HasColumnName("UtworzonoUTC")
                .HasDefaultValueSql("sysutcdatetime()");

            builder.Property(e => e.ZmienionoUtc)
                .HasColumnName("ZmienionoUTC")
                .HasDefaultValueSql("sysutcdatetime()");

            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

            // Strategia TPT (Table Per Type) - kluczowa linia!
            builder.UseTptMappingStrategy();
        }
    }
}