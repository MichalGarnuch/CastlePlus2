using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastlePlus2.Contracts.DTOs.Rdzen
{
    /// <summary>
    /// Obiekt transferu danych (DTO) dla Nieruchomości.
    /// Służy do komunikacji API <-> Frontend. Nie zawiera logiki biznesowej.
    /// </summary>
    public class NieruchomoscDto
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; } = string.Empty;

        // Id FK do adresu w bazie
        public long? IdAdresuGlownego { get; set; }

        // Zagnieżdżony DTO z pełnymi danymi adresu (opcjonalny).
        public AdresDto? AdresGlowny { get; set; }

        public long? IdPodmiotuWlasciciela { get; set; }

        // Geometrii na razie nie wystawiamy do tego DTO.
    }
}
