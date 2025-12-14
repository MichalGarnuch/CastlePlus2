using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastlePlus2.Domain.Entities.Slowniki
{
    /// <summary>
    /// Słownik jednostek miary (np. m2, szt, mies).
    /// Tabela: [slowniki].[JednostkaMiary]
    /// PK: KodJednostki (nvarchar(20))
    /// </summary>
    public class JednostkaMiary
    {
        public string KodJednostki { get; set; } = default!;
        public string Nazwa { get; set; } = default!;
    }
}

