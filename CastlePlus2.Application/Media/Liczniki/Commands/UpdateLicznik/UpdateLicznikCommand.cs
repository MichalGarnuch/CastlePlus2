using MediatR;

namespace CastlePlus2.Application.Media.Liczniki.Commands.UpdateLicznik
{
    public class UpdateLicznikCommand : IRequest<bool>
    {
        public long IdLicznika { get; set; }

        public long IdPrzylacza { get; set; }
        public long? IdLicznikaNadrzednego { get; set; }

        public string NumerNV { get; set; } = string.Empty;
        public string KodJednostki { get; set; } = string.Empty;

        public decimal? WspolczynnikPrzeliczeniowy { get; set; }
        public bool Aktywny { get; set; }
    }
}