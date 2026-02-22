using CastlePlus2.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Auth
{
    /// <summary>
    /// Konfiguracja mapowania encji Uzytkownik na tabelę [auth].[Uzytkownik].
    /// </summary>
    public class UzytkownikConfiguration : IEntityTypeConfiguration<Uzytkownik>
    {
        public void Configure(EntityTypeBuilder<Uzytkownik> builder)
        {
            builder.ToTable("Uzytkownik", "auth");

            builder.HasKey(x => x.IdUzytkownika);

            builder.Property(x => x.IdUzytkownika)
                   .HasColumnName("IdUzytkownika")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Login)
                   .HasColumnName("Login")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Email)
                   .HasColumnName("Email")
                   .HasMaxLength(200)
                   .IsRequired(false);

            builder.Property(x => x.HasloHash)
                   .HasColumnName("HasloHash")
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(x => x.CzyAktywny)
                   .HasColumnName("CzyAktywny")
                   .IsRequired();

            builder.Property(x => x.CzyUsuniety)
                   .HasColumnName("CzyUsuniety")
                   .IsRequired();

            builder.Property(x => x.UsunietoUtc)
                   .HasColumnName("UsunietoUtc")
                   .HasColumnType("datetime2(6)")
                   .IsRequired(false);

            builder.Property(x => x.UsunietoPrzez)
                   .HasColumnName("UsunietoPrzez")
                   .HasMaxLength(200)
                   .IsRequired(false);

            builder.Property(x => x.DataUtworzeniaUtc)
                   .HasColumnName("DataUtworzeniaUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired();

            builder.Property(x => x.DataModyfikacjiUtc)
                   .HasColumnName("DataModyfikacjiUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired();

            builder.Property(x => x.OstatnieLogowanieUtc)
                   .HasColumnName("OstatnieLogowanieUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired(false);

            builder.HasIndex(x => x.Login)
                   .IsUnique();

            builder.HasIndex(x => x.Email)
                   .IsUnique()
                   .HasFilter("[Email] IS NOT NULL");
        }
    }
}