using CastlePlus2.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Auth
{
    public sealed class ActivationTokenConfiguration : IEntityTypeConfiguration<ActivationToken>
    {
        public void Configure(EntityTypeBuilder<ActivationToken> builder)
        {
            builder.ToTable("ActivationToken", "auth");

            builder.HasKey(x => x.IdActivationToken);

            builder.Property(x => x.IdActivationToken)
                   .HasColumnName("IdActivationToken")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.IdUzytkownika)
                   .HasColumnName("IdUzytkownika")
                   .IsRequired();

            builder.Property(x => x.TokenHash)
                   .HasColumnName("TokenHash")
                   .HasMaxLength(64)
                   .IsRequired();

            builder.Property(x => x.CreatedAtUtc)
                   .HasColumnName("CreatedAtUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired();

            builder.Property(x => x.ExpiresAtUtc)
                   .HasColumnName("ExpiresAtUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired();

            builder.Property(x => x.UsedAtUtc)
                   .HasColumnName("UsedAtUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired(false);

            builder.HasIndex(x => x.IdUzytkownika);
            builder.HasIndex(x => x.ExpiresAtUtc);
        }
    }
}