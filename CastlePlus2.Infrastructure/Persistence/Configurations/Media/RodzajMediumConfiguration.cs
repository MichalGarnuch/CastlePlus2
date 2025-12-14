using CastlePlus2.Domain.Entities.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Media
{
    /// <summary>
    /// Konfiguracja EF Core dla [media].[RodzajMedium].
    /// </summary>
    public class RodzajMediumConfiguration : IEntityTypeConfiguration<RodzajMedium>
    {
        public void Configure(EntityTypeBuilder<RodzajMedium> builder)
        {
            builder.ToTable("RodzajMedium", "media");

            builder.HasKey(x => x.KodRodzaju)
                   .HasName("PK_me_Rodzaj");

            builder.Property(x => x.KodRodzaju)
                   .HasColumnName("KodRodzaju")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.Nazwa)
                   .HasColumnName("Nazwa")
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}
