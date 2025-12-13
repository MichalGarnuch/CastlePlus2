using CastlePlus2.Domain.Entities.Dokumenty;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Dokumenty
{
    /// <summary>
    /// Konfiguracja EF Core dla [dokumenty].[PowiazanieDokumentu].
    /// </summary>
    public class PowiazanieDokumentuConfiguration : IEntityTypeConfiguration<PowiazanieDokumentu>
    {
        public void Configure(EntityTypeBuilder<PowiazanieDokumentu> builder)
        {
            builder.ToTable("PowiazanieDokumentu", "dokumenty");

            builder.HasKey(x => x.IdPowiazania)
                   .HasName("PK_dok_Powiazanie");

            builder.Property(x => x.IdPowiazania)
                   .HasColumnName("IdPowiazania")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.IdDokumentu)
                   .HasColumnName("IdDokumentu")
                   .IsRequired();

            builder.Property(x => x.IdEncji)
                   .HasColumnName("IdEncji")
                   .IsRequired();

            // Indeksy z SQL
            builder.HasIndex(x => x.IdDokumentu)
                   .HasDatabaseName("IX_dok_Powiazanie_Dokument");

            builder.HasIndex(x => x.IdEncji)
                   .HasDatabaseName("IX_dok_Powiazanie_Encja");

            // FK -> dokumenty.Dokument
            builder.HasOne(x => x.Dokument)
                   .WithMany()
                   .HasForeignKey(x => x.IdDokumentu)
                   .HasConstraintName("FK_dok_Powiazanie_Dokument")
                   .OnDelete(DeleteBehavior.Restrict);

            // FK -> rdzen.Encja
            builder.HasOne(x => x.Encja)
                   .WithMany()
                   .HasForeignKey(x => x.IdEncji)
                   .HasConstraintName("FK_dok_Powiazanie_Encja")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
