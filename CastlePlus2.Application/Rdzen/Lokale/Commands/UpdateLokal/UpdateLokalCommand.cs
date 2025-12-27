using MediatR;

namespace CastlePlus2.Application.Rdzen.Lokale.Commands.UpdateLokal
{
    // Standard: Command płaski, Controller mapuje Request -> Command
    public sealed class UpdateLokalCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public Guid IdBudynku { get; set; }
        public string KodLokalu { get; set; } = string.Empty;

        public decimal? Powierzchnia { get; set; }
        public string? Przeznaczenie { get; set; }
    }
}