using CastlePlus2.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Auth
{
    /// <summary>
    /// Konfiguracja mapowania encji UzytkownikRola na tabelę [auth].[UzytkownikRola].
    /// </summary>
    public class UzytkownikRolaConfiguration : IEntityTypeConfiguration<UzytkownikRola>
    {
        public void Configure(EntityTypeBuilder<UzytkownikRola> builder)
        {
            builder.ToTable("UzytkownikRola", "auth");

            builder.HasKey(x => new { x.IdUzytkownika, x.IdRoli });

            builder.Property(x => x.IdUzytkownika)
                   .HasColumnName("IdUzytkownika")
                   .IsRequired();

            builder.Property(x => x.IdRoli)
                   .HasColumnName("IdRoli")
                   .IsRequired();

            builder.HasOne(x => x.Uzytkownik)
                   .WithMany(x => x.UzytkownikRole)
                   .HasForeignKey(x => x.IdUzytkownika)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Rola)
                   .WithMany(x => x.UzytkownikRole)
                   .HasForeignKey(x => x.IdRoli)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.IdRoli);
        }
    }
}