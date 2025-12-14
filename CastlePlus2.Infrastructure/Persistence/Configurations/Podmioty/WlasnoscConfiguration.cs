using CastlePlus2.Domain.Entities.Podmioty;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Podmioty
{
    /// <summary>
    /// Konfiguracja EF Core dla [podmioty].[Wlasnosc].
    /// </summary>
    public class WlasnoscConfiguration : IEntityTypeConfiguration<Wlasnosc>
    {
        public void Configure(EntityTypeBuilder<Wlasnosc> builder)
        {
            builder.ToTable("Wlasnosc", "podmioty");

            builder.HasKey(x => x.IdWlasnosci)
                   .HasName("PK_po_Wlasnosc");

            builder.Property(x => x.IdWlasnosci)
                   .HasColumnName("IdWlasnosci");

            builder.Property(x => x.IdEncji)
                   .HasColumnName("IdEncji")
                   .IsRequired();

            builder.Property(x => x.IdPodmiotu)
                   .HasColumnName("IdPodmiotu")
                   .IsRequired();

            builder.Property(x => x.UdzialProcent)
                   .HasColumnName("UdzialProcent")
                   .HasPrecision(7, 4)
                   .IsRequired();

            // SQL: date -> w projekcie trzymamy jako DateOnly
            builder.Property(x => x.OdDnia)
                   .HasColumnName("OdDnia")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(x => x.DoDnia)
                   .HasColumnName("DoDnia")
                   .HasColumnType("date");

            // timestamp/rowversion – token współbieżności (EF sam to ogarnia)
            builder.Property(x => x.RowVersion)
                   .HasColumnName("RowVersion")
                   .IsRowVersion()
                   .IsConcurrencyToken();
        }
    }
}
