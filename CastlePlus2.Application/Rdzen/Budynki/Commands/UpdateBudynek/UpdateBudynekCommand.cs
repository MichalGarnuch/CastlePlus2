using MediatR;

namespace CastlePlus2.Application.Rdzen.Budynki.Commands.UpdateBudynek
{
    // Standard: Command płaski, Controller mapuje Request -> Command
    public sealed class UpdateBudynekCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public Guid IdNieruchomosci { get; set; }
        public string KodBudynku { get; set; } = string.Empty;

        public long? IdAdresu { get; set; }
        public short? Kondygnacje { get; set; }
        public decimal? PowierzchniaUzytkowa { get; set; }
    }
}
