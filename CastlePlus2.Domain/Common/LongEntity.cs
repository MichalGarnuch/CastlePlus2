using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastlePlus2.Domain.Common
{
    /// <summary>
    /// Klasa bazowa dla tabel finansowych i słownikowych (np. Faktura, Waluta),
    /// które w bazie używają [bigint] lub [int] jako klucza.
    /// </summary>
    public abstract class LongEntity : BaseEntity<long>
    {
    }
}
