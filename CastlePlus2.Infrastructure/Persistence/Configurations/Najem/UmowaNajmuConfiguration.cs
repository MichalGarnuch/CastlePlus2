using CastlePlus2.Domain.Entities.Najem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Najem
{
    /// <summary>
    /// Mapowanie EF Core dla [najem].[UmowaNajmu]
    /// </summary>
    public class UmowaNajmuConfiguration : IEntityTypeConfiguration<UmowaNajmu>
    {
        public void Configure(EntityTypeBuilder<UmowaNajmu> builder)
        {
            // TPT – PK/RowVersion jest w EncjaConfiguration (bazowa Encja).
            builder.ToTable("UmowaNajmu", "najem");

            builder.Property(x => x.IdWynajmujacego)
                   .HasColumnName("IdWynajmujacego")
                   .IsRequired();

            builder.Property(x => x.IdNajemcy)
                   .HasColumnName("IdNajemcy")
                   .IsRequired();

            builder.Property(x => x.DataZawarcia)
                   .HasColumnName("DataZawarcia")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(x => x.DataPoczatku)
                   .HasColumnName("DataPoczatku")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(x => x.DataZakonczenia)
                   .HasColumnName("DataZakonczenia")
                   .HasColumnType("date")
                   .IsRequired(false);

            builder.Property(x => x.KodWaluty)
                   .HasColumnName("KodWaluty")
                   .HasColumnType("char(3)")
                   .IsRequired();

            builder.Property(x => x.KodIndeksacji)
                   .HasColumnName("KodIndeksacji")
                   .HasMaxLength(20)
                   .IsRequired(false);

            // FK są w bazie: do [podmioty].[Podmiot] oraz [slowniki].[Waluta] i [slowniki].[Indeksacja]
            // Nie musimy dodawać nawigacji, żeby testy API działały.
        }
    }
}
