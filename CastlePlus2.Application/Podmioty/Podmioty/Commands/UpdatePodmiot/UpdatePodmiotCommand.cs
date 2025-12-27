using MediatR;

namespace CastlePlus2.Application.Podmioty.Podmioty.Commands.UpdatePodmiot
{
    // Standard: Command płaski, Controller mapuje Request -> Command
    public class UpdatePodmiotCommand : IRequest<bool>
    {
        public long IdPodmiotu { get; set; }

        public string Nazwa { get; set; } = string.Empty;
        public string? NIP { get; set; }
        public string? REGON { get; set; }
        public string? PESEL { get; set; }
        public string TypPodmiotu { get; set; } = string.Empty;
    }
}