using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Rdzen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Rdzen
{
    /// <summary>
    /// Konfiguracja mapowania encji Nieruchomosc na tabelę [rdzen].[Nieruchomosc].
    /// 
    /// Zasada:
    /// - Wszystkie szczegóły mapowania (nazwy tabel, schemat, długości pól, typy)
    ///   trzymamy w klasach konfiguracji, a nie rozsypane po DbContext.
    /// - Dzięki temu model jest czytelny, łatwiej go utrzymać i testować.
    /// </summary>
    public class NieruchomoscConfiguration : IEntityTypeConfiguration<Nieruchomosc>
    {
        public void Configure(EntityTypeBuilder<Nieruchomosc> builder)
        {
            // Mapowanie do tabeli i schematu SQL
            builder.ToTable("Nieruchomosc", "rdzen");

            // Klucz główny – w encji używamy właściwości Id (z GuidEntity),
            // która jest zmapowana na kolumnę [IdEncji] przez atrybut [Column].
            builder.HasKey(n => n.Id);

            // Nazwa – NVARCHAR(200) NOT NULL
            builder.Property(n => n.Nazwa)
                   .IsRequired()               // NOT NULL
                   .HasMaxLength(200);         // nvarchar(200)

            // IdAdresuGlownego – BIGINT NULL
            builder.Property(n => n.IdAdresuGlownego)
                   .IsRequired(false);

            // IdPodmiotuWlasciciela – BIGINT NULL
            builder.Property(n => n.IdPodmiotuWlasciciela)
                   .IsRequired(false);

            // Geometria – typ geography w SQL Server (NetTopologySuite)
            // Uwaga:
            // - Zakładamy, że w bazie jest kolumna [Geometria] [geography] NULL.
            // - EF Core + NetTopologySuite obsłużą to jako Geometry.
            builder.Property(n => n.Geometria)
                   .HasColumnType("geography") // jawne określenie typu SQL
                   .IsRequired(false);         // dopuszczamy NULL

            // RowVersion – kolumna typu [timestamp] w bazie,
            // używana jako token współbieżności optymistycznej.
            //
            // W klasie bazowej BaseEntity<TKey> mamy właściwość RowVersion (byte[]).
            // Tutaj oznaczamy ją jako concurrency token i RowVersion.
            builder.Property(n => n.RowVersion)
                   .IsRowVersion()             // informuje EF, że to kolumna rowversion/timestamp
                   .IsConcurrencyToken();      // używana do kontroli współbieżności

            // Tutaj będziemy później dodawać konfigurację relacji (FK) do Adresu, Podmiotu,
            // Budynków itd., gdy encje tych tabel zostaną stworzone.
            //
            // Przykład na przyszłość:
            // builder.HasOne(n => n.AdresGlowny)
            //        .WithMany()
            //        .HasForeignKey(n => n.IdAdresuGlownego);
        }
    }
}
