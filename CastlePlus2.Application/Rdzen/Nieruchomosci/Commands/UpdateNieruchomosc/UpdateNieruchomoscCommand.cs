using MediatR;

namespace CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.UpdateNieruchomosc
{
    // Standard: Command płaski, Controller mapuje Request -> Command
    public sealed class UpdateNieruchomoscCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public string Nazwa { get; set; } = string.Empty;
        public long? IdAdresuGlownego { get; set; }
        public long? IdPodmiotuWlasciciela { get; set; }
    }
}