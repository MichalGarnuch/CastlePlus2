using CastlePlus2.Domain.Entities.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Media
{
    /// <summary>
    /// Konfiguracja EF Core dla [media].[Licznik].
    /// </summary>
    public class LicznikConfiguration : IEntityTypeConfiguration<Licznik>
    {
        public void Configure(EntityTypeBuilder<Licznik> builder)
        {
            builder.ToTable("Licznik", "media");

            builder.HasKey(x => x.IdLicznika)
                   .HasName("PK_me_Licznik");

            builder.Property(x => x.IdLicznika)
                   .HasColumnName("IdLicznika");

            builder.Property(x => x.IdPrzylacza)
                   .HasColumnName("IdPrzylacza")
                   .IsRequired();

            builder.Property(x => x.IdLicznikaNadrzednego)
                   .HasColumnName("IdLicznikaNadrzednego");

            builder.Property(x => x.NumerNV)
                   .HasColumnName("NumerNV")
                   .HasMaxLength(60)
                   .IsRequired();

            builder.Property(x => x.KodJednostki)
                   .HasColumnName("KodJednostki")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.WspolczynnikPrzeliczeniowy)
                   .HasColumnName("WspolczynnikPrzeliczeniowy")
                   .HasPrecision(12, 6);

            builder.Property(x => x.Aktywny)
                   .HasColumnName("Aktywny")
                   .IsRequired();

            // Unikalność numeru licznika (UX_me_Licznik_Numer na NumerNV)
            builder.HasIndex(x => x.NumerNV)
                   .IsUnique()
                   .HasDatabaseName("UX_me_Licznik_Numer");

            // FK -> media.Przylacze
            builder.HasOne(x => x.Przylacze)
                   .WithMany() // nie zakładamy kolekcji po drugiej stronie, żeby nie mieszać feature’ów
                   .HasForeignKey(x => x.IdPrzylacza)
                   .HasConstraintName("FK_me_Licznik_Przylacze")
                   .OnDelete(DeleteBehavior.Restrict);

            // Self FK -> Licznik nadrzędny
            builder.HasOne(x => x.LicznikNadrzedny)
                   .WithMany()
                   .HasForeignKey(x => x.IdLicznikaNadrzednego)
                   .HasConstraintName("FK_me_Licznik_Nadrzedny")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
