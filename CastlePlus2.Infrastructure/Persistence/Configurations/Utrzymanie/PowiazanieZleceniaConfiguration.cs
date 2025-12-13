using CastlePlus2.Domain.Entities.Utrzymanie;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Utrzymanie
{
    /// <summary>
    /// Konfiguracja EF Core dla [utrzymanie].[PowiazanieZlecenia].
    /// </summary>
    public class PowiazanieZleceniaConfiguration : IEntityTypeConfiguration<PowiazanieZlecenia>
    {
        public void Configure(EntityTypeBuilder<PowiazanieZlecenia> builder)
        {
            builder.ToTable("PowiazanieZlecenia", "utrzymanie");

            builder.HasKey(x => x.IdPowiazania)
                   .HasName("PK_ut_PowiazanieZlecenia");

            builder.Property(x => x.IdPowiazania)
                   .HasColumnName("IdPowiazania")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.IdZlecenia)
                   .HasColumnName("IdZlecenia")
                   .IsRequired();

            builder.Property(x => x.IdEncji)
                   .HasColumnName("IdEncji")
                   .IsRequired();

            // FK -> rdzen.Encja
            builder.HasOne(x => x.Encja)
                   .WithMany()
                   .HasForeignKey(x => x.IdEncji)
                   .HasConstraintName("FK_ut_Powiazanie_Encja")
                   .OnDelete(DeleteBehavior.Restrict);

            // FK -> utrzymanie.ZleceniePracy
            builder.HasOne(x => x.ZleceniePracy)
                   .WithMany()
                   .HasForeignKey(x => x.IdZlecenia)
                   .HasConstraintName("FK_ut_Powiazanie_Zlecenie")
                   .OnDelete(DeleteBehavior.Restrict);

            // Indeksy (jeżeli istnieją w SQL – warto je odzwierciedlić)
            builder.HasIndex(x => x.IdEncji).HasDatabaseName("IX_ut_Powiazanie_Encja");
            builder.HasIndex(x => x.IdZlecenia).HasDatabaseName("IX_ut_Powiazanie_Zlecenie");
        }
    }
}
