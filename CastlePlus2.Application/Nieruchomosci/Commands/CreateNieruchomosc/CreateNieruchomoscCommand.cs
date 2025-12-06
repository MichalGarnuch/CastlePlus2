using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CastlePlus2.Application.Nieruchomosci.Commands.CreateNieruchomosc
{
    /// <summary>
    /// Komenda CQRS odpowiedzialna za utworzenie nowej nieruchomości.
    /// 
    /// Wzorzec:
    /// - Command = prosty obiekt z danymi wejściowymi (bez logiki).
    /// - Handler = osobna klasa, która realizuje operację (logika biznesowa).
    /// 
    /// Ta komenda zwraca Guid – Id nowo utworzonej nieruchomości.
    /// </summary>
    public class CreateNieruchomoscCommand : IRequest<Guid>
    {
        /// <summary>
        /// Nazwa nieruchomości (wymagana).
        /// </summary>
        public string Nazwa { get; set; } = string.Empty;

        /// <summary>
        /// Id adresu głównego (FK do tabeli Adres), opcjonalne.
        /// </summary>
        public long? IdAdresuGlownego { get; set; }

        /// <summary>
        /// Id podmiotu-właściciela, opcjonalne.
        /// </summary>
        public long? IdPodmiotuWlasciciela { get; set; }

        // Geometria specjalnie pominięta w pierwszej wersji Command,
        // żeby nie komplikować startu (mapy, WKT, itp. dojdą później).
        // Możemy dodać tu np. string WktGeometria w kolejnej iteracji.
    }
}

