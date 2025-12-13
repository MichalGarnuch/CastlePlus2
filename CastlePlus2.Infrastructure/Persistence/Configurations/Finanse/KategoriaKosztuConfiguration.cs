using CastlePlus2.Domain.Entities.Finanse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Finanse
{
    /// <summary>
    /// Konfiguracja EF Core dla [finanse].[KategoriaKosztu].
    /// </summary>
    public class KategoriaKosztuConfiguration : IEntityTypeConfiguration<KategoriaKosztu>
    {
        public void Configure(EntityTypeBuilder<KategoriaKosztu> builder)
        {
            builder.ToTable("KategoriaKosztu", "finanse");

            builder.HasKey(x => x.IdKategoriiKosztu)
                   .HasName("PK_fi_KategoriaKosztu");

            builder.Property(x => x.IdKategoriiKosztu)
                   .HasColumnName("IdKategoriiKosztu")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Kod)
                   .HasColumnName("Kod")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.Nazwa)
                   .HasColumnName("Nazwa")
                   .HasMaxLength(100)
                   .IsRequired();

            // Unikalny indeks na Kod (UX_fi_KategoriaKod)
            builder.HasIndex(x => x.Kod)
                   .IsUnique()
                   .HasDatabaseName("UX_fi_KategoriaKod");
        }
    }
}
