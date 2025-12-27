using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.CreateNieruchomosc
{
    // Standard: Command płaski, Controller mapuje Request -> Command
    public class CreateNieruchomoscCommand : IRequest<NieruchomoscDto>
    {
        public string Nazwa { get; set; } = string.Empty;
        public long? IdAdresuGlownego { get; set; }
        public long? IdPodmiotuWlasciciela { get; set; }
    }
}