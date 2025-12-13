using CastlePlus2.Domain.Entities.Podmioty;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Podmioty
{
    /// <summary>
    /// Mapowanie EF Core dla [podmioty].[Podmiot]
    /// </summary>
    public class PodmiotConfiguration : IEntityTypeConfiguration<Podmiot>
    {
        public void Configure(EntityTypeBuilder<Podmiot> builder)
        {
            builder.ToTable("Podmiot", "podmioty");

            builder.HasKey(x => x.IdPodmiotu)
                   .HasName("PK_po_Podmiot");

            builder.Property(x => x.IdPodmiotu)
                   .HasColumnName("IdPodmiotu")
                   .ValueGeneratedOnAdd(); // IDENTITY(1,1)

            builder.Property(x => x.Nazwa)
                   .HasColumnName("Nazwa")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.NIP)
                   .HasColumnName("NIP")
                   .HasMaxLength(20)
                   .IsRequired(false);

            builder.Property(x => x.REGON)
                   .HasColumnName("REGON")
                   .HasMaxLength(20)
                   .IsRequired(false);

            builder.Property(x => x.PESEL)
                   .HasColumnName("PESEL")
                   .HasMaxLength(11)
                   .IsRequired(false);

            builder.Property(x => x.TypPodmiotu)
                   .HasColumnName("TypPodmiotu")
                   .HasMaxLength(30)
                   .IsRequired();
        }
    }
}
