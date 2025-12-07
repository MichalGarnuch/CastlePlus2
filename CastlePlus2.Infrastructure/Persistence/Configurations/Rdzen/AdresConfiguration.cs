using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Rdzen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Rdzen
{
    /// <summary>
    /// Konfiguracja mapowania encji Adres na tabelę [rdzen].[Adres] w SQL.
    /// </summary>
    public class AdresConfiguration : IEntityTypeConfiguration<Adres>
    {
        public void Configure(EntityTypeBuilder<Adres> builder)
        {
            builder.ToTable("Adres", "rdzen");

            // Klucz główny: IdAdresu (IDENTITY)
            builder.HasKey(a => a.IdAdresu);

            builder.Property(a => a.IdAdresu)
                   .HasColumnName("IdAdresu")
                   .ValueGeneratedOnAdd(); // IDENTITY(1,1)

            builder.Property(a => a.Kraj)
                   .IsRequired()
                   .HasMaxLength(60);

            builder.Property(a => a.Wojewodztwo)
                   .HasMaxLength(60);

            builder.Property(a => a.Powiat)
                   .HasMaxLength(60);

            builder.Property(a => a.Gmina)
                   .HasMaxLength(60);

            builder.Property(a => a.Miejscowosc)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.Ulica)
                   .HasMaxLength(100);

            builder.Property(a => a.Numer)
                   .HasMaxLength(20);

            builder.Property(a => a.KodPocztowy)
                   .HasMaxLength(12);

            builder.Property(a => a.AdresPelny)
                   .HasMaxLength(300);
        }
    }
}

