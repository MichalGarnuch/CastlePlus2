using CastlePlus2.Domain.Entities.Media;
using CastlePlus2.Domain.Entities.Rdzen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Media
{
    /// <summary>
    /// Konfiguracja EF Core dla [media].[Przylacze].
    /// </summary>
    public class PrzylaczeConfiguration : IEntityTypeConfiguration<Przylacze>
    {
        public void Configure(EntityTypeBuilder<Przylacze> builder)
        {
            builder.ToTable("Przylacze", "media");

            builder.HasKey(x => x.IdPrzylacza)
                   .HasName("PK_me_Przylacze");

            builder.Property(x => x.IdPrzylacza)
                   .HasColumnName("IdPrzylacza");

            builder.Property(x => x.IdEncjiGospodarza)
                   .IsRequired()
                   .HasColumnName("IdEncjiGospodarza");

            builder.Property(x => x.KodRodzaju)
                   .IsRequired()
                   .HasColumnName("KodRodzaju")
                   .HasMaxLength(20);

            builder.Property(x => x.Opis)
                   .HasColumnName("Opis")
                   .HasMaxLength(200);

            // Indeks z bazy
            builder.HasIndex(x => x.IdEncjiGospodarza)
                   .HasDatabaseName("IX_me_Przylacze_Encja");

            // FK -> rdzen.Encja (IdEncjiGospodarza)
            builder.HasOne(x => x.EncjaGospodarza)
                   .WithMany()
                   .HasForeignKey(x => x.IdEncjiGospodarza)
                   .OnDelete(DeleteBehavior.Restrict);

            // FK -> media.RodzajMedium (KodRodzaju)
            builder.HasOne(x => x.RodzajMedium)
                   .WithMany()
                   .HasForeignKey(x => x.KodRodzaju)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
