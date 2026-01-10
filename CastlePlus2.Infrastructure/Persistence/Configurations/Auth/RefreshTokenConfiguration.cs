using CastlePlus2.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Auth
{
    /// <summary>
    /// Konfiguracja mapowania encji RefreshToken na tabelę [auth].[RefreshToken].
    /// </summary>
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshToken", "auth");

            builder.HasKey(x => x.IdRefreshToken);

            builder.Property(x => x.IdRefreshToken)
                   .HasColumnName("IdRefreshToken")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.IdUzytkownika)
                   .HasColumnName("IdUzytkownika")
                   .IsRequired();

            builder.Property(x => x.TokenHash)
                   .HasColumnName("TokenHash")
                   .HasColumnType("varbinary(32)")
                   .IsRequired();

            builder.Property(x => x.ExpiresAtUtc)
                   .HasColumnName("ExpiresAtUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired();

            builder.Property(x => x.RevokedAtUtc)
                   .HasColumnName("RevokedAtUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired(false);

            builder.Property(x => x.CreatedAtUtc)
                   .HasColumnName("CreatedAtUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired();

            builder.Property(x => x.DeviceInfo)
                   .HasColumnName("DeviceInfo")
                   .HasMaxLength(200)
                   .IsRequired(false);

            builder.Property(x => x.IpAddress)
                   .HasColumnName("IpAddress")
                   .HasMaxLength(45)
                   .IsRequired(false);

            builder.HasOne(x => x.Uzytkownik)
                   .WithMany(x => x.RefreshTokens)
                   .HasForeignKey(x => x.IdUzytkownika)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.IdUzytkownika);
            builder.HasIndex(x => x.TokenHash)
                   .IsUnique();
        }
    }
}