using CastlePlus2.Domain.Entities.Najem;
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

            builder.HasKey(x => x.IdKaucji)
                   .HasName("PK_nj_Kaucja");

            builder.Property(x => x.IdKaucji)
                   .HasColumnName("IdKaucji")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.IdUmowyNajmu)
                   .HasColumnName("IdUmowyNajmu")
                   .IsRequired();

            builder.Property(x => x.Kwota)
                   .HasColumnName("Kwota")
                   .HasColumnType("decimal(12,2)")
                   .IsRequired();

            builder.Property(x => x.KodWaluty)
                   .HasColumnName("KodWaluty")
                   .HasColumnType("char(3)")
                   .IsRequired();

            builder.Property(x => x.DataWplaty)
                   .HasColumnName("DataWplaty")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(x => x.DataZwrotu)
                   .HasColumnName("DataZwrotu")
                   .HasColumnType("date")
                   .IsRequired(false);

            builder.Property(x => x.Status)
                   .HasColumnName("Status")
                   .HasMaxLength(30)
                   .IsRequired();
        }
    }
}
