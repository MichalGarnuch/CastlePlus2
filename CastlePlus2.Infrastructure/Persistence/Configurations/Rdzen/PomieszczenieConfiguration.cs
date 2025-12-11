using CastlePlus2.Domain.Entities.Rdzen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Rdzen
{
    /// <summary>
    /// Konfiguracja EF Core dla [rdzen].[Pomieszczenie].
    /// </summary>
    public class PomieszczenieConfiguration : IEntityTypeConfiguration<Pomieszczenie>
    {
        public void Configure(EntityTypeBuilder<Pomieszczenie> builder)
        {
            // TPT – klucz i RowVersion ogarnia EncjaConfiguration.
            builder.ToTable("Pomieszczenie", "rdzen");

            // Unikalny indeks: (IdEncjiNadrzednej, KodPomieszczenia)
            builder.HasIndex(p => new { p.IdEncjiNadrzednej, p.KodPomieszczenia })
                   .IsUnique()
                   .HasDatabaseName("UX_rd_Pomieszczenie_Kod_Lokal");

            // Relacja 1 Lokal -> wiele Pomieszczeń
            builder.HasOne(p => p.Lokal)
                   .WithMany(l => l.Pomieszczenia)
                   .HasForeignKey(p => p.IdEncjiNadrzednej)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
