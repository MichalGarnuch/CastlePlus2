using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastlePlus2.Domain.Entities.Slowniki
{
    /// <summary>
    /// Słownik indeksacji (np. CPI, stała, brak).
    /// Tabela: [slowniki].[Indeksacja]
    /// PK: KodIndeksacji (nvarchar(20))
    /// </summary>
    public class Indeksacja
    {
        public string KodIndeksacji { get; set; } = default!;
        public string Nazwa { get; set; } = default!;
    }
}

