using CastlePlus2.Domain.Entities.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Media
{
    /// <summary>
    /// Konfiguracja EF Core dla [media].[Odczyt].
    /// </summary>
    public class OdczytConfiguration : IEntityTypeConfiguration<Odczyt>
    {
        public void Configure(EntityTypeBuilder<Odczyt> builder)
        {
            builder.ToTable("Odczyt", "media");

            builder.HasKey(x => x.IdOdczytu)
                   .HasName("PK_me_Odczyt");

            builder.Property(x => x.IdOdczytu)
                   .HasColumnName("IdOdczytu");

            builder.Property(x => x.IdLicznika)
                   .HasColumnName("IdLicznika")
                   .IsRequired();

            // SQL date -> mapujemy jako DateTime i pilnujemy, że czas jest 00:00:00
            builder.Property(x => x.DataOdczytu)
                   .HasColumnName("DataOdczytu")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(x => x.Wskazanie)
                   .HasColumnName("Wskazanie")
                   .HasPrecision(18, 6)
                   .IsRequired();

            builder.Property(x => x.Zrodlo)
                   .HasColumnName("Zrodlo")
                   .HasMaxLength(20);

            // Unikalność z DB: (IdLicznika, DataOdczytu)
            builder.HasIndex(x => new { x.IdLicznika, x.DataOdczytu })
                   .IsUnique()
                   .HasDatabaseName("UX_me_Odczyt_LicznikData");

            // FK do Licznika
            builder.HasOne<Licznik>()
                   .WithMany()
                   .HasForeignKey(x => x.IdLicznika)
                   .HasConstraintName("FK_me_Odczyt_Licznik")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
