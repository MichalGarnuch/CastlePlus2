using CastlePlus2.Domain.Entities.Rdzen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Rdzen
{
    /// <summary>
    /// Konfiguracja mapowania encji Lokal na tabelę [rdzen].[Lokal].
    /// </summary>
    public class LokalConfiguration : IEntityTypeConfiguration<Lokal>
    {
        public void Configure(EntityTypeBuilder<Lokal> builder)
        {
            // TPT – klucz główny i RowVersion są skonfigurowane w EncjaConfiguration.
            builder.ToTable("Lokal", "rdzen");

            // Kolumny specyficzne
            builder.Property(l => l.IdBudynku)
                   .HasColumnName("IdBudynku")
                   .IsRequired();

            builder.Property(l => l.KodLokalu)
                   .HasColumnName("KodLokalu")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(l => l.Powierzchnia)
                   .HasColumnName("Powierzchnia")
                   .HasColumnType("decimal(12,2)")
                   .IsRequired(false);

            builder.Property(l => l.Przeznaczenie)
                   .HasColumnName("Przeznaczenie")
                   .HasMaxLength(60)
                   .IsRequired(false);

            // Relacja: Lokal -> Budynek
            builder.HasOne(l => l.Budynek)
                   .WithMany(b => b.Lokale)
                   .HasForeignKey(l => l.IdBudynku)
                   .OnDelete(DeleteBehavior.Restrict);

            // Unikalność (IdBudynku, KodLokalu) zgodnie z indeksem UX_rd_Lokal_Kod_Bud
            builder.HasIndex(l => new { l.IdBudynku, l.KodLokalu })
                   .IsUnique();
        }
    }
}
