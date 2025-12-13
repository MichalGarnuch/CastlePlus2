using CastlePlus2.Domain.Entities.Finanse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Finanse
{
    /// <summary>
    /// Konfiguracja EF Core dla [finanse].[RozliczeniePlatnosci].
    /// </summary>
    public class RozliczeniePlatnosciConfiguration : IEntityTypeConfiguration<RozliczeniePlatnosci>
    {
        public void Configure(EntityTypeBuilder<RozliczeniePlatnosci> builder)
        {
            builder.ToTable("RozliczeniePlatnosci", "finanse");

            builder.HasKey(x => x.IdRozliczenia)
                   .HasName("PK_fi_Rozliczenie");

            builder.Property(x => x.IdRozliczenia)
                   .HasColumnName("IdRozliczenia")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.IdPlatnosci)
                   .HasColumnName("IdPlatnosci")
                   .IsRequired();

            builder.Property(x => x.IdFaktury)
                   .HasColumnName("IdFaktury")
                   .IsRequired();

            builder.Property(x => x.Kwota)
                   .HasColumnName("Kwota")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Indeksy
            builder.HasIndex(x => x.IdPlatnosci)
                   .HasDatabaseName("IX_fi_Rozliczenie_IdPlatnosci");

            builder.HasIndex(x => x.IdFaktury)
                   .HasDatabaseName("IX_fi_Rozliczenie_IdFaktury");

            // FK -> finanse.Platnosc
            builder.HasOne(x => x.Platnosc)
                   .WithMany()
                   .HasForeignKey(x => x.IdPlatnosci)
                   .HasConstraintName("FK_fi_Rozliczenie_Platnosc")
                   .OnDelete(DeleteBehavior.Restrict);

            // FK -> finanse.Faktura
            builder.HasOne(x => x.Faktura)
                   .WithMany()
                   .HasForeignKey(x => x.IdFaktury)
                   .HasConstraintName("FK_fi_Rozliczenie_Faktura")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
