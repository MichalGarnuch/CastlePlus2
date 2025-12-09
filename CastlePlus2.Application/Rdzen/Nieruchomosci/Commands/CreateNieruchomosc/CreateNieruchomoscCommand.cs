using System;
using MediatR;
using CastlePlus2.Contracts.DTOs.Rdzen;

namespace CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.CreateNieruchomosc
{
    /// <summary>
    /// Komenda CQRS odpowiedzialna za utworzenie nowej nieruchomości.
    /// Zwraca NieruchomoscDto, tak jak CreateBudynekCommand zwraca BudynekDto.
    /// </summary>
    public class CreateNieruchomoscCommand : IRequest<NieruchomoscDto>
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
