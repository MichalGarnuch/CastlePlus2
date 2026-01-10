using CastlePlus2.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Auth
{
    /// <summary>
    /// Konfiguracja mapowania encji Rola na tabelę [auth].[Rola].
    /// </summary>
    public class RolaConfiguration : IEntityTypeConfiguration<Rola>
    {
        public void Configure(EntityTypeBuilder<Rola> builder)
        {
            builder.ToTable("Rola", "auth");

            builder.HasKey(x => x.IdRoli);

            builder.Property(x => x.IdRoli)
                   .HasColumnName("IdRoli")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Kod)
                   .HasColumnName("Kod")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.Nazwa)
                   .HasColumnName("Nazwa")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Opis)
                   .HasColumnName("Opis")
                   .HasMaxLength(250)
                   .IsRequired(false);

            builder.HasIndex(x => x.Kod)
                   .IsUnique();
        }
    }
}