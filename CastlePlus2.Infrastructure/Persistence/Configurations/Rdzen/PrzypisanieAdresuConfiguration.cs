using CastlePlus2.Domain.Entities.Rdzen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Rdzen
{
    /// <summary>
    /// Konfiguracja EF Core dla [rdzen].[PrzypisanieAdresu].
    /// Uwaga: tabela NIE posiada kolumny RowVersion, więc encja nie dziedziczy po LongEntity.
    /// </summary>
    public class PrzypisanieAdresuConfiguration : IEntityTypeConfiguration<PrzypisanieAdresu>
    {
        public void Configure(EntityTypeBuilder<PrzypisanieAdresu> builder)
        {
            builder.ToTable("PrzypisanieAdresu", "rdzen");

            builder.HasKey(x => x.IdPrzypisaniaAdresu)
                   .HasName("PK_rd_PrzyAdres");

            builder.Property(x => x.IdPrzypisaniaAdresu)
                   .HasColumnName("IdPrzypisaniaAdresu")
                   .ValueGeneratedOnAdd(); // IDENTITY w SQL

            builder.Property(x => x.IdAdresu)
                   .HasColumnName("IdAdresu")
                   .IsRequired();

            builder.Property(x => x.IdEncji)
                   .HasColumnName("IdEncji")
                   .IsRequired();

            // Kolumny typu 'date' w SQL – dla DateOnly ustawiamy typ date.
            builder.Property(x => x.OdDnia)
                   .HasColumnName("OdDnia")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(x => x.DoDnia)
                   .HasColumnName("DoDnia")
                   .HasColumnType("date")
                   .IsRequired(false);

            // FK: Adres
            builder.HasOne(x => x.Adres)
                   .WithMany()
                   .HasForeignKey(x => x.IdAdresu)
                   .HasConstraintName("FK_rd_PrzyAdres_Adres")
                   .OnDelete(DeleteBehavior.Restrict);

            // FK: Encja
            builder.HasOne(x => x.Encja)
                   .WithMany()
                   .HasForeignKey(x => x.IdEncji)
                   .HasConstraintName("FK_rd_PrzyAdres_Encja")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.IdAdresu).HasDatabaseName("IX_rd_PrzyAdres_IdAdresu");
            builder.HasIndex(x => x.IdEncji).HasDatabaseName("IX_rd_PrzyAdres_IdEncji");
        }
    }
}
