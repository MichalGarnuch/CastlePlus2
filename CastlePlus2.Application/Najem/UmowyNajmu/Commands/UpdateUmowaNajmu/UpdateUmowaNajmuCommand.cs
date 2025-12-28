using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Commands.UpdateUmowaNajmu
{
    public sealed class UpdateUmowaNajmuCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public long IdWynajmujacego { get; set; }
        public long IdNajemcy { get; set; }

        public DateTime DataZawarcia { get; set; }
        public DateTime DataPoczatku { get; set; }
        public DateTime? DataZakonczenia { get; set; }

        public string KodWaluty { get; set; } = string.Empty;
        public string? KodIndeksacji { get; set; }
    }
}