using MediatR;

namespace CastlePlus2.Application.Najem.SkladnikiCzynszu.Commands.UpdateSkladnikCzynszu
{
    // Standard: Command płaski, Controller mapuje Request -> Command
    public sealed class UpdateSkladnikCzynszuCommand : IRequest<bool>
    {
        public long IdSkladnikaCzynszu { get; set; }

        public Guid IdUmowyNajmu { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public string KodJednostki { get; set; } = string.Empty;
        public decimal Stawka { get; set; }
        public decimal? IloscBazowa { get; set; }
        public string? KodIndeksacji { get; set; }
        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }
    }
}