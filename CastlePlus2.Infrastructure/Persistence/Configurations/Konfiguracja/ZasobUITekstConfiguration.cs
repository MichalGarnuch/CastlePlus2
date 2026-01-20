using CastlePlus2.Domain.Entities.Konfiguracja;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Konfiguracja
{
    /// <summary>
    /// Konfiguracja EF Core dla tabeli [konfiguracja].[ZasobUITekst].
    /// </summary>
    public class ZasobUITekstConfiguration : IEntityTypeConfiguration<ZasobUITekst>
    {
        public void Configure(EntityTypeBuilder<ZasobUITekst> builder)
        {
            builder.ToTable("ZasobUITekst", "konfiguracja");

            builder.HasKey(x => x.IdZasobuTekstu)
                   .HasName("PK_konf_ZasobUITekst");

            builder.Property(x => x.IdZasobuTekstu)
                   .HasColumnName("IdZasobuTekstu")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.IdEncji)
                   .HasColumnName("IdEncji")
                   .IsRequired();

            builder.Property(x => x.Jezyk)
                   .HasColumnName("Jezyk")
                   .HasMaxLength(10)
                   .HasDefaultValue("pl-PL")
                   .IsRequired();

            builder.Property(x => x.Pole)
                   .HasColumnName("Pole")
                   .HasMaxLength(40)
                   .HasDefaultValue("Value")
                   .IsRequired();

            builder.Property(x => x.Wartosc)
                   .HasColumnName("Wartosc")
                   .IsRequired();

            builder.Property(x => x.Format)
                   .HasColumnName("Format")
                   .HasMaxLength(20)
                   .HasDefaultValue("Plain")
                   .IsRequired();

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

            builder.HasIndex(x => new { x.IdEncji, x.Jezyk, x.Pole })
                   .IsUnique()
                   .HasDatabaseName("UQ_konf_ZasobUITekst_Encja_Jezyk_Pole");

            builder.HasIndex(x => new { x.Jezyk, x.Pole })
                   .HasDatabaseName("IX_konf_ZasobUITekst_Jezyk_Pole")
                   .IncludeProperties(x => x.IdEncji);

            builder.HasOne(x => x.ZasobUI)
                   .WithMany(x => x.Teksty)
                   .HasForeignKey(x => x.IdEncji)
                   .OnDelete(DeleteBehavior.NoAction)
                   .HasConstraintName("FK_konf_ZasobUITekst_ZasobUI");
        }
    }
}