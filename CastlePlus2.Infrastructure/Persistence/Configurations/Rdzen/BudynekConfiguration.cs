using CastlePlus2.Domain.Entities.Rdzen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Rdzen
{
    /// <summary>
    /// Konfiguracja mapowania encji Budynek na tabelę [rdzen].[Budynek].
    /// </summary>
    public class BudynekConfiguration : IEntityTypeConfiguration<Budynek>
    {
        public void Configure(EntityTypeBuilder<Budynek> builder)
        {
            // TPT – klucz główny i RowVersion są skonfigurowane w EncjaConfiguration.
            builder.ToTable("Budynek", "rdzen");

            // Wymagane właściwości opisowe
            builder.Property(b => b.KodBudynku)
                   .IsRequired()
                   .HasMaxLength(50);

            // Jedna kolumna Kondygnacje (smallint NULL) – zgodnie z SQL
            builder.Property(b => b.Kondygnacje)
                   .HasColumnName("Kondygnacje")
                   .IsRequired(false);

            builder.Property(b => b.PowierzchniaUzytkowa)
                   .HasColumnType("decimal(12,2)")
                   .IsRequired(false);

            builder.Property(b => b.IdAdresu)
                   .IsRequired(false);

            // Relacja: Budynek -> Nieruchomosc (FK: IdNieruchomosci)
            builder.HasOne(b => b.Nieruchomosc)
                   .WithMany() // na razie bez kolekcji Budynkow w Nieruchomosc
                   .HasForeignKey(b => b.IdNieruchomosci)
                   .OnDelete(DeleteBehavior.Restrict);
            // Restrict = odpowiada NO ACTION w SQL (nie kasujemy budynków „w łańcuchu”).

            // Relacja: Budynek -> Adres (FK: IdAdresu)
            builder.HasOne(b => b.Adres)
                   .WithMany() // Adres nie musi wiedzieć, które budynki z niego korzystają
                   .HasForeignKey(b => b.IdAdresu)
                   .OnDelete(DeleteBehavior.Restrict);

            // Unikalność (IdNieruchomosci, KodBudynku) – jak w indeksie w SQL
            builder.HasIndex(b => new { b.IdNieruchomosci, b.KodBudynku })
                   .IsUnique();
        }
    }
}
