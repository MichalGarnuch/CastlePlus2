using CastlePlus2.Domain.Entities.Rdzen;          // <= DODAJ TO
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

            // FK -> rdzen.Encja (IdEncji)
            builder.HasOne(x => x.Encja)
                   .WithMany()
                   .HasForeignKey(x => x.IdEncji)
                   .HasConstraintName("FK_ut_PowiazanieZlecenia_Encja")
                   .OnDelete(DeleteBehavior.NoAction);

            // FK -> utrzymanie.ZleceniePracy (IdZlecenia)
            builder.HasOne(x => x.ZleceniePracy)
                   .WithMany(x => x.Powiazania)
                   .HasForeignKey(x => x.IdZlecenia)
                   .HasConstraintName("FK_ut_PowiazanieZlecenia_ZleceniePracy")
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(x => x.IdEncji)
                   .HasDatabaseName("IX_ut_PowiazanieZlecenia_IdEncji");

            builder.HasIndex(x => x.IdZlecenia)
                   .HasDatabaseName("IX_ut_PowiazanieZlecenia_IdZlecenia");
        }
    }
}
