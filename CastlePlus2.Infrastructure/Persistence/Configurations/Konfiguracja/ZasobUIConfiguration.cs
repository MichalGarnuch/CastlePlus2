using CastlePlus2.Domain.Entities.Konfiguracja;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Konfiguracja
{
    /// <summary>
    /// Konfiguracja EF Core dla tabeli [konfiguracja].[ZasobUI].
    /// </summary>
    public class ZasobUIConfiguration : IEntityTypeConfiguration<ZasobUI>
    {
        public void Configure(EntityTypeBuilder<ZasobUI> builder)
        {
            builder.ToTable("ZasobUI", "konfiguracja");

            builder.HasKey(x => x.IdEncji)
                   .HasName("PK_konf_ZasobUI");

            builder.Property(x => x.IdEncji)
                   .HasColumnName("IdEncji")
                   .IsRequired();

            builder.Property(x => x.KodZasobu)
                   .HasColumnName("KodZasobu")
                   .HasMaxLength(120)
                   .IsRequired();

            builder.Property(x => x.Typ)
                   .HasColumnName("Typ")
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(x => x.Kategoria)
                   .HasColumnName("Kategoria")
                   .HasMaxLength(60)
                   .IsRequired(false);

            builder.Property(x => x.CzyAktywny)
                   .HasColumnName("CzyAktywny")
                   .HasDefaultValue(true)
                   .IsRequired();

            builder.Property(x => x.Sort)
                   .HasColumnName("Sort")
                   .HasDefaultValue(0)
                   .IsRequired();

            builder.Property(x => x.WazneOdUtc)
                   .HasColumnName("WazneOdUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired(false);

            builder.Property(x => x.WazneDoUtc)
                   .HasColumnName("WazneDoUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired(false);

            builder.Property(x => x.UtworzonoUtc)
                   .HasColumnName("UtworzonoUtc")
                   .HasColumnType("datetime2(0)")
                   .HasDefaultValueSql("sysutcdatetime()")
                   .IsRequired();

            builder.Property(x => x.ZmienionoUtc)
                   .HasColumnName("ZmienionoUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired(false);

            builder.Property(x => x.RowVersion)
                   .HasColumnName("RowVersion")
                   .IsRowVersion();

            builder.HasIndex(x => x.KodZasobu)
                   .IsUnique()
                   .HasDatabaseName("UQ_konf_ZasobUI_KodZasobu");

            builder.HasIndex(x => new { x.Typ, x.Kategoria, x.CzyAktywny })
                   .HasDatabaseName("IX_konf_ZasobUI_Typ_Kategoria_Aktywny");

            builder.HasOne(x => x.Encja)
                   .WithMany()
                   .HasForeignKey(x => x.IdEncji)
                   .OnDelete(DeleteBehavior.NoAction)
                   .HasConstraintName("FK_konf_ZasobUI_Encja");

            builder.HasMany(x => x.Teksty)
                   .WithOne(x => x.ZasobUI)
                   .HasForeignKey(x => x.IdEncji)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}