using CastlePlus2.Domain.Entities.Dokumenty;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Dokumenty
{
    /// <summary>
    /// Konfiguracja EF Core dla tabeli [dokumenty].[Dokument].
    /// </summary>
    public class DokumentConfiguration : IEntityTypeConfiguration<Dokument>
    {
        public void Configure(EntityTypeBuilder<Dokument> builder)
        {
            builder.ToTable("Dokument", "dokumenty");

            builder.HasKey(x => x.IdDokumentu)
                   .HasName("PK_dok_Dokument");

            builder.Property(x => x.IdDokumentu)
                   .HasColumnName("IdDokumentu")
                   .ValueGeneratedOnAdd(); // IDENTITY(1,1)

            builder.Property(x => x.IdEncjiOwner)
                   .HasColumnName("IdEncjiOwner")
                   .IsRequired(false);

            builder.Property(x => x.Nazwa)
                   .HasColumnName("Nazwa")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Opis)
                   .HasColumnName("Opis")
                   .HasMaxLength(1000)
                   .IsRequired(false);

            builder.Property(x => x.SciezkaPliku)
                   .HasColumnName("SciezkaPliku")
                   .HasMaxLength(400)
                   .IsRequired();

            builder.Property(x => x.DataUtworzenia)
                   .HasColumnName("DataUtworzenia")
                   .HasColumnType("datetime2(0)")
                   .HasDefaultValueSql("sysutcdatetime()")
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            // Index z SQL: IX_dok_Dokument_EncjaOwner
            builder.HasIndex(x => x.IdEncjiOwner)
                   .HasDatabaseName("IX_dok_Dokument_EncjaOwner");

            // FK: Dokument -> EncjaOwner (NO ACTION w SQL)
            builder.HasOne<CastlePlus2.Domain.Entities.Rdzen.Encja>()
                   .WithMany()
                   .HasForeignKey(x => x.IdEncjiOwner)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_dok_Dokument_EncjaOwner");
        }
    }
}
