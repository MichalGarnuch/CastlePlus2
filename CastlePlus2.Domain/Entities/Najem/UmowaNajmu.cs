using System;
using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Domain.Entities.Najem
{
    /// <summary>
    /// Umowa najmu. Tabela: [najem].[UmowaNajmu]
    /// PK/FK: IdEncji (uniqueidentifier) -> [rdzen].[Encja]
    /// </summary>
    public class UmowaNajmu : Encja
    {
        public long IdWynajmujacego { get; set; }
        public long IdNajemcy { get; set; }

        public DateTime DataZawarcia { get; set; }
        public DateTime DataPoczatku { get; set; }
        public DateTime? DataZakonczenia { get; set; }

        public string KodWaluty { get; set; } = default!;
        public string? KodIndeksacji { get; set; }
    }
}
