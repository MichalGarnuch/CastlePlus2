using CastlePlus2.Domain.Entities.Finanse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Finanse
{
    /// <summary>
    /// Konfiguracja EF Core dla [finanse].[AlokacjaKosztu].
    /// </summary>
    public class AlokacjaKosztuConfiguration : IEntityTypeConfiguration<AlokacjaKosztu>
    {
        public void Configure(EntityTypeBuilder<AlokacjaKosztu> builder)
        {
            builder.ToTable("AlokacjaKosztu", "finanse");

            builder.HasKey(x => x.IdAlokacji)
                   .HasName("PK_fi_AlokacjaKosztu");

            builder.Property(x => x.IdAlokacji)
                   .HasColumnName("IdAlokacji")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.IdPozycjiKosztu)
                   .HasColumnName("IdPozycjiKosztu")
                   .IsRequired();

            builder.Property(x => x.IdEncji)
                   .HasColumnName("IdEncji")
                   .IsRequired();

            builder.Property(x => x.KwotaNetto)
                   .HasColumnName("KwotaNetto")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.KwotaBrutto)
                   .HasColumnName("KwotaBrutto")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // FK -> rdzen.Encja
            builder.HasOne(x => x.Encja)
                   .WithMany()
                   .HasForeignKey(x => x.IdEncji)
                   .HasConstraintName("FK_fi_Alokacja_Encja")
                   .OnDelete(DeleteBehavior.Restrict);

            // FK -> finanse.PozycjaKosztu
            //builder.HasOne(x => x.PozycjaKosztu)
            //       .WithMany()
            //       .HasForeignKey(x => x.IdPozycjiKosztu)
            //       .HasConstraintName("FK_fi_Alokacja_Pozycja")
            //       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
