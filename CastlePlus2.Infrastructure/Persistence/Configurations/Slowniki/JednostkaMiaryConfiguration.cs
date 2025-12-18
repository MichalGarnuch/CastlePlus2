using CastlePlus2.Domain.Entities.Slowniki;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Slowniki
{
    /// <summary>
    /// Mapowanie EF Core dla [slowniki].[JednostkaMiary]
    /// </summary>
    public class JednostkaMiaryConfiguration : IEntityTypeConfiguration<JednostkaMiary>
    {
        public void Configure(EntityTypeBuilder<JednostkaMiary> builder)
        {
            builder.ToTable("JednostkaMiary", "slowniki");

            builder.HasKey(x => x.KodJednostki)
                   .HasName("PK_sl_JednostkaMiary");

            builder.Property(x => x.KodJednostki)
                   .HasColumnName("KodJednostki")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.Nazwa)
                   .HasColumnName("Nazwa")
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}
