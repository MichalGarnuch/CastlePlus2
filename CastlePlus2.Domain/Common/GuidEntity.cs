using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastlePlus2.Domain.Common
{
    /// <summary>
    /// Klasa bazowa dla tabel "rdzeniowych" (np. Nieruchomosc, Budynek),
    /// które w bazie używają [uniqueidentifier] jako klucza.
    /// </summary>
    public abstract class GuidEntity : BaseEntity<Guid>
    {
        // Tutaj możemy dodać wspólne pola dla rdzenia, jeśli się pojawią (np. Audit info)
    }
}
