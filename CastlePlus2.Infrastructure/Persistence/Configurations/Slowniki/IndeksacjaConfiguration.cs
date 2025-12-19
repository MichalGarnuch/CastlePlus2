using CastlePlus2.Domain.Entities.Slowniki;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Slowniki
{
    /// <summary>
    /// Mapowanie EF Core dla [slowniki].[Indeksacja]
    /// </summary>
    public class IndeksacjaConfiguration : IEntityTypeConfiguration<Indeksacja>
    {
        public void Configure(EntityTypeBuilder<Indeksacja> builder)
        {
            builder.ToTable("Indeksacja", "slowniki");

            builder.HasKey(x => x.KodIndeksacji)
                   .HasName("PK_sl_Indeksacja");

            builder.Property(x => x.KodIndeksacji)
                   .HasColumnName("KodIndeksacji")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.Nazwa)
                   .HasColumnName("Nazwa")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.AdresZrodlaURL)
                   .HasColumnName("AdresZrodlaURL")
                   .HasMaxLength(400)
                   .IsRequired(false);
        }
    }
}
