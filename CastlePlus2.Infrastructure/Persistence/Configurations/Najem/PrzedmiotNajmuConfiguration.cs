using CastlePlus2.Domain.Entities.Najem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CastlePlus2.Domain.Entities.Rdzen;


namespace CastlePlus2.Infrastructure.Persistence.Configurations.Najem
{
    /// <summary>
    /// Mapowanie EF Core dla [najem].[PrzedmiotNajmu]
    /// </summary>
    public class PrzedmiotNajmuConfiguration : IEntityTypeConfiguration<PrzedmiotNajmu>
    {
        public void Configure(EntityTypeBuilder<PrzedmiotNajmu> builder)
        {
            builder.ToTable("PrzedmiotNajmu", "najem");

            builder.HasKey(x => x.IdPrzedmiotuNajmu)
                   .HasName("PK_nj_Przedmiot");

            builder.Property(x => x.IdPrzedmiotuNajmu)
                   .HasColumnName("IdPrzedmiotuNajmu")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.IdUmowyNajmu)
                   .HasColumnName("IdUmowyNajmu")
                   .IsRequired();

            builder.Property(x => x.IdEncji)
                   .HasColumnName("IdEncji")
                   .IsRequired();

            builder.Property(x => x.UdzialProcent)
                   .HasColumnName("UdzialProcent")
                   .HasColumnType("decimal(7,4)")
                   .IsRequired(false);

            builder.Property(x => x.OdDnia)
                   .HasColumnName("OdDnia")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(x => x.DoDnia)
                   .HasColumnName("DoDnia")
                   .HasColumnType("date")
                   .IsRequired(false);

            // FK from SQL: FK_nj_Przedmiot_Umowa (IdUmowyNajmu -> najem.UmowaNajmu.IdEncji)
            builder.HasOne<UmowaNajmu>()
                .WithMany()
                .HasForeignKey(x => x.IdUmowyNajmu)
                .OnDelete(DeleteBehavior.NoAction);

            // FK from SQL: FK_nj_Przedmiot_Encja (IdEncji -> rdzen.Encja.IdEncji)
            builder.HasOne<Encja>()
                .WithMany()
                .HasForeignKey(x => x.IdEncji)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
