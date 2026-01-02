using CastlePlus2.Domain.Entities.Najem;
using CastlePlus2.Domain.Entities.Slowniki;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Najem
{
    /// <summary>
    /// Mapowanie EF Core dla [najem].[Kaucja]
    /// </summary>
    public class KaucjaConfiguration : IEntityTypeConfiguration<Kaucja>
    {
        public void Configure(EntityTypeBuilder<Kaucja> builder)
        {
            builder.ToTable("Kaucja", "najem");

            builder.HasKey(x => x.IdOperacjiKaucji)
                   .HasName("PK_nj_Kaucja");

            builder.Property(x => x.IdOperacjiKaucji)
                   .HasColumnName("IdOperacjiKaucji")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.IdUmowyNajmu)
                   .HasColumnName("IdUmowyNajmu")
                   .IsRequired();

            builder.Property(x => x.RodzajOperacji)
                   .HasColumnName("RodzajOperacji")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.Kwota)
                   .HasColumnName("Kwota")
                   .HasColumnType("decimal(12,2)")
                   .IsRequired();

            builder.Property(x => x.KodWaluty)
                   .HasColumnName("KodWaluty")
                   .HasColumnType("char(3)")
                   .IsRequired();

            builder.Property(x => x.DataOperacji)
                   .HasColumnName("DataOperacji")
                   .HasColumnType("date")
                   .IsRequired();

            // FK from SQL: FK_nj_Kaucja_Umowa (IdUmowyNajmu -> najem.UmowaNajmu.IdEncji)
            builder.HasOne<UmowaNajmu>()
                .WithMany()
                .HasForeignKey(x => x.IdUmowyNajmu)
                .OnDelete(DeleteBehavior.NoAction);

            // FK from SQL: FK_nj_Kaucja_Waluta (KodWaluty -> slowniki.Waluta.KodWaluty)
            builder.HasOne<Waluta>()
                .WithMany()
                .HasForeignKey(x => x.KodWaluty)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}