using CastlePlus2.Domain.Entities.Slowniki;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Slowniki
{
    /// <summary>
    /// Mapowanie EF Core dla [slowniki].[Waluta]
    /// </summary>
    public class WalutaConfiguration : IEntityTypeConfiguration<Waluta>
    {
        public void Configure(EntityTypeBuilder<Waluta> builder)
        {
            builder.ToTable("Waluta", "slowniki");

            builder.HasKey(x => x.KodWaluty)
                   .HasName("PK_sl_Waluta");

            builder.Property(x => x.KodWaluty)
                   .HasColumnName("KodWaluty")
                   .HasColumnType("char(3)")
                   .IsRequired();

            builder.Property(x => x.Nazwa)
                   .HasColumnName("Nazwa")
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
