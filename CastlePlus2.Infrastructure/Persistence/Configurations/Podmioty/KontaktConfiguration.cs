using CastlePlus2.Domain.Entities.Podmioty;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Podmioty
{
    /// <summary>
    /// Konfiguracja EF Core dla [podmioty].[Kontakt].
    /// </summary>
    public class KontaktConfiguration : IEntityTypeConfiguration<Kontakt>
    {
        public void Configure(EntityTypeBuilder<Kontakt> builder)
        {
            builder.ToTable("Kontakt", "podmioty");

            builder.HasKey(x => x.IdKontaktu)
                   .HasName("PK_po_Kontakt");

            builder.Property(x => x.IdKontaktu)
                   .HasColumnName("IdKontaktu")
                   .ValueGeneratedOnAdd(); // IDENTITY(1,1)

            builder.Property(x => x.IdPodmiotu)
                   .HasColumnName("IdPodmiotu")
                   .IsRequired();

            builder.Property(x => x.Rodzaj)
                   .HasColumnName("Rodzaj")
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(x => x.Wartosc)
                   .HasColumnName("Wartosc")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.HasOne<Podmiot>()
                   .WithMany()
                   .HasForeignKey(x => x.IdPodmiotu)
                   .HasConstraintName("FK_po_Kontakt_Podmiot")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
