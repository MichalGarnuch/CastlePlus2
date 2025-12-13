using CastlePlus2.Domain.Entities.Finanse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Finanse
{
    /// <summary>
    /// Konfiguracja EF Core dla [finanse].[PozycjaKosztu].
    /// </summary>
    public class PozycjaKosztuConfiguration : IEntityTypeConfiguration<PozycjaKosztu>
    {
        public void Configure(EntityTypeBuilder<PozycjaKosztu> builder)
        {
            builder.ToTable("PozycjaKosztu", "finanse");

            builder.HasKey(x => x.IdPozycjiKosztu)
                   .HasName("PK_fi_PozycjaKosztu");

            builder.Property(x => x.IdPozycjiKosztu)
                   .HasColumnName("IdPozycjiKosztu")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.IdFaktury)
                   .HasColumnName("IdFaktury")
                   .IsRequired();

            builder.Property(x => x.IdKategoriiKosztu)
                   .HasColumnName("IdKategoriiKosztu")
                   .IsRequired();

            builder.Property(x => x.Opis)
                   .HasColumnName("Opis")
                   .HasMaxLength(200)
                   .IsRequired(false);

            builder.Property(x => x.KwotaNetto)
                   .HasColumnName("KwotaNetto")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.KwotaBrutto)
                   .HasColumnName("KwotaBrutto")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Indeksy z SQL
            builder.HasIndex(x => x.IdFaktury)
                   .HasDatabaseName("IX_fi_Pozycja_IdFaktury");

            builder.HasIndex(x => x.IdKategoriiKosztu)
                   .HasDatabaseName("IX_fi_Pozycja_IdKategorii");

            // FK -> finanse.Faktura
            builder.HasOne(x => x.Faktura)
                   .WithMany()
                   .HasForeignKey(x => x.IdFaktury)
                   .HasConstraintName("FK_fi_Pozycja_Faktura")
                   .OnDelete(DeleteBehavior.Restrict);

            // FK -> finanse.KategoriaKosztu
            builder.HasOne(x => x.KategoriaKosztu)
                   .WithMany()
                   .HasForeignKey(x => x.IdKategoriiKosztu)
                   .HasConstraintName("FK_fi_Pozycja_Kategoria")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
