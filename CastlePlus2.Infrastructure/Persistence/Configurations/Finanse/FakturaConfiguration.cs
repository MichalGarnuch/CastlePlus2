using CastlePlus2.Domain.Entities.Finanse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Finanse
{
    /// <summary>
    /// Konfiguracja EF Core dla [finanse].[Faktura].
    /// </summary>
    public class FakturaConfiguration : IEntityTypeConfiguration<Faktura>
    {
        public void Configure(EntityTypeBuilder<Faktura> builder)
        {
            builder.ToTable("Faktura", "finanse");

            builder.HasKey(x => x.IdFaktury)
                   .HasName("PK_fi_Faktura");

            builder.Property(x => x.IdFaktury)
                   .HasColumnName("IdFaktury")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.NumerFaktury)
                   .HasColumnName("NumerFaktury")
                   .HasMaxLength(60)
                   .IsRequired();

            builder.Property(x => x.IdPodmiotu)
                   .HasColumnName("IdPodmiotu")
                   .IsRequired();

            builder.Property(x => x.DataWystawienia)
                   .HasColumnName("DataWystawienia")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(x => x.DataSprzedazy)
                   .HasColumnName("DataSprzedazy")
                   .HasColumnType("date");

            builder.Property(x => x.KodWaluty)
                   .HasColumnName("KodWaluty")
                   .HasColumnType("char(3)")
                   .IsRequired();

            builder.Property(x => x.KwotaNetto)
                   .HasColumnName("KwotaNetto")
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.KwotaBrutto)
                   .HasColumnName("KwotaBrutto")
                   .HasColumnType("decimal(18,2)");

            // Indeksy z SQL
            builder.HasIndex(x => x.IdPodmiotu)
                   .HasDatabaseName("IX_fi_Faktura_Podmiot");

            builder.HasIndex(x => x.NumerFaktury)
                   .IsUnique()
                   .HasDatabaseName("UX_fi_Faktura_Numer");


            // pamiętać żeby potem odkomentować
            // FK -> podmioty.Podmiot
            //builder.HasOne(x => x.Podmiot)
            //       .WithMany()
            //       .HasForeignKey(x => x.IdPodmiotu)
            //       .HasConstraintName("FK_fi_Faktura_Podmiot")
            //       .OnDelete(DeleteBehavior.Restrict);

            //// FK -> slowniki.Waluta (KodWaluty)
            //builder.HasOne(x => x.Waluta)
            //       .WithMany()
            //       .HasForeignKey(x => x.KodWaluty)
            //       .HasConstraintName("FK_fi_Faktura_Waluta")
            //       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
