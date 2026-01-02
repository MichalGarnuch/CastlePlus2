using CastlePlus2.Domain.Entities.Najem;
using CastlePlus2.Domain.Entities.Slowniki;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Najem
{
    /// <summary>
    /// Mapowanie EF Core dla [najem].[SkladnikCzynszu]
    /// </summary>
    public class SkladnikCzynszuConfiguration : IEntityTypeConfiguration<SkladnikCzynszu>
    {
        public void Configure(EntityTypeBuilder<SkladnikCzynszu> builder)
        {
            builder.ToTable("SkladnikCzynszu", "najem");

            builder.HasKey(x => x.IdSkladnikaCzynszu)
                   .HasName("PK_nj_Skladnik");

            builder.Property(x => x.IdSkladnikaCzynszu)
                   .HasColumnName("IdSkladnikaCzynszu")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.IdUmowyNajmu)
                   .HasColumnName("IdUmowyNajmu")
                   .IsRequired();

            builder.Property(x => x.Nazwa)
                   .HasColumnName("Nazwa")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.KodJednostki)
                   .HasColumnName("KodJednostki")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.Stawka)
                   .HasColumnName("Stawka")
                   .HasColumnType("decimal(12,4)")
                   .IsRequired();

            builder.Property(x => x.IloscBazowa)
                   .HasColumnName("IloscBazowa")
                   .HasColumnType("decimal(12,4)")
                   .IsRequired(false);

            builder.Property(x => x.KodIndeksacji)
                   .HasColumnName("KodIndeksacji")
                   .HasMaxLength(20)
                   .IsRequired(false);

            builder.Property(x => x.OdDnia)
                   .HasColumnName("OdDnia")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(x => x.DoDnia)
                   .HasColumnName("DoDnia")
                   .HasColumnType("date")
                   .IsRequired(false);

            // FK from SQL: FK_nj_Skladnik_Umowa (IdUmowyNajmu -> najem.UmowaNajmu.IdEncji)
            builder.HasOne<UmowaNajmu>()
                .WithMany()
                .HasForeignKey(x => x.IdUmowyNajmu)
                .OnDelete(DeleteBehavior.NoAction);

            // FK from SQL: FK_nj_Skladnik_Jednostka (KodJednostki -> slowniki.JednostkaMiary.KodJednostki)
            builder.HasOne<JednostkaMiary>()
                .WithMany()
                .HasForeignKey(x => x.KodJednostki)
                .OnDelete(DeleteBehavior.NoAction);

            // FK from SQL: FK_nj_Skladnik_Indeks (KodIndeksacji -> slowniki.Indeksacja.KodIndeksacji)
            builder.HasOne<Indeksacja>()
                .WithMany()
                .HasForeignKey(x => x.KodIndeksacji)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
