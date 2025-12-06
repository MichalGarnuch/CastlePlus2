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

        // Na potrzeby listy (Grid) często potrzebujemy od razu nazwy adresu/właściciela, 
        // a nie tylko ich ID. To uzupełnimy później w logice AutoMappera.
        public long? IdAdresuGlownego { get; set; }
        public long? IdPodmiotuWlasciciela { get; set; }

        // Geometrii na razie nie wystawiamy do DTO prostego, 
        // chyba że będzie potrzebna na mapie.
    }
}
