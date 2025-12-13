using CastlePlus2.Domain.Entities.Utrzymanie;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Utrzymanie
{
    /// <summary>
    /// Mapowanie EF Core dla [utrzymanie].[ZleceniePracy].
    /// </summary>
    public class ZleceniePracyConfiguration : IEntityTypeConfiguration<ZleceniePracy>
    {
        public void Configure(EntityTypeBuilder<ZleceniePracy> builder)
        {
            builder.ToTable("ZleceniePracy", "utrzymanie");

            builder.HasKey(x => x.IdZlecenia)
                   .HasName("PK_ut_ZleceniePracy");

            builder.Property(x => x.IdZlecenia)
                   .HasColumnName("IdZlecenia")
                   .ValueGeneratedOnAdd(); // IDENTITY

            builder.Property(x => x.IdEncjiGospodarza)
                   .HasColumnName("IdEncjiGospodarza")
                   .IsRequired();

            builder.Property(x => x.Tytul)
                   .HasColumnName("Tytul")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Opis)
                   .HasColumnName("Opis")
                   .HasMaxLength(1000)
                   .IsRequired(false);

            builder.Property(x => x.Status)
                   .HasColumnName("Status")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.DataUtworzenia)
                   .HasColumnName("DataUtworzenia")
                   .HasColumnType("datetime2(0)")
                   .HasDefaultValueSql("sysutcdatetime()")
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(x => x.DataZamkniecia)
                   .HasColumnName("DataZamkniecia")
                   .HasColumnType("datetime2(0)")
                   .IsRequired(false);

            // Indeks jak w SQL (przyspiesza wyszukiwanie po gospodarzu)
            builder.HasIndex(x => x.IdEncjiGospodarza)
                   .HasDatabaseName("IX_ut_Zlecenie_EncjaGospodarz");
        }
    }
}
