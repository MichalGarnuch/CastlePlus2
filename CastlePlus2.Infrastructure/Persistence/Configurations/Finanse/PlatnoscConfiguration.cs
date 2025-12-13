using CastlePlus2.Domain.Entities.Finanse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Finanse
{
    /// <summary>
    /// Konfiguracja EF Core dla [finanse].[Platnosc].
    /// </summary>
    public class PlatnoscConfiguration : IEntityTypeConfiguration<Platnosc>
    {
        public void Configure(EntityTypeBuilder<Platnosc> builder)
        {
            builder.ToTable("Platnosc", "finanse");

            builder.HasKey(x => x.IdPlatnosci)
                   .HasName("PK_fi_Platnosc");

            builder.Property(x => x.IdPlatnosci)
                   .HasColumnName("IdPlatnosci")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.IdPodmiotu)
                   .HasColumnName("IdPodmiotu")
                   .IsRequired();

            builder.Property(x => x.DataPlatnosci)
                   .HasColumnName("DataPlatnosci")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(x => x.KodWaluty)
                   .HasColumnName("KodWaluty")
                   .HasColumnType("char(3)")
                   .IsRequired();

            builder.Property(x => x.Kwota)
                   .HasColumnName("Kwota")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Indeks z SQL: IX_fi_Platnosc_Podmiot
            builder.HasIndex(x => x.IdPodmiotu)
                   .HasDatabaseName("IX_fi_Platnosc_Podmiot");

            //// FK -> podmioty.Podmiot
            builder.HasOne(x => x.Podmiot)
                   .WithMany()
                   .HasForeignKey(x => x.IdPodmiotu)
                   .HasConstraintName("FK_fi_Platnosc_Podmiot")
                   .OnDelete(DeleteBehavior.Restrict);

            //// FK -> slowniki.Waluta (KodWaluty)
            builder.HasOne(x => x.Waluta)
                   .WithMany()
                   .HasForeignKey(x => x.KodWaluty)
                   .HasConstraintName("FK_fi_Platnosc_Waluta")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
