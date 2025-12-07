using CastlePlus2.Domain.Entities.Rdzen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Rdzen
{
    public class NieruchomoscConfiguration : IEntityTypeConfiguration<Nieruchomosc>
    {
        public void Configure(EntityTypeBuilder<Nieruchomosc> builder)
        {
            // W TPT wskazujemy tylko nazwę tabeli potomnej.
            // EF Core automatycznie połączy ją z Encją po Id.
            builder.ToTable("Nieruchomosc", "rdzen");

            // Pola specyficzne dla Nieruchomości
            builder.Property(n => n.Nazwa)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(n => n.IdAdresuGlownego)
                   .IsRequired(false);

            builder.Property(n => n.IdPodmiotuWlasciciela)
                   .IsRequired(false);

            builder.Property(n => n.Geometria)
                   .HasColumnType("geography")
                   .IsRequired(false);

            // Relacja FK: Nieruchomosc.IdAdresuGlownego -> Adres.IdAdresu
            builder.HasOne(n => n.AdresGlowny)
                   .WithMany() // brak nawigacji zwrotnej po stronie Adres
                   .HasForeignKey(n => n.IdAdresuGlownego)
                   .OnDelete(DeleteBehavior.Restrict);
            // Restrict = odpowiada domyślnemu NO ACTION w Twoim schemacie.
        }
    }
}