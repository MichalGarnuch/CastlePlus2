using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Liczniki.Commands.CreateLicznik
{
    public class CreateLicznikCommand : IRequest<LicznikDto>
    {
        public long IdPrzylacza { get; set; }
        public long? IdLicznikaNadrzednego { get; set; }

        public string NumerNV { get; set; } = string.Empty;
        public string KodJednostki { get; set; } = string.Empty;

        public decimal? WspolczynnikPrzeliczeniowy { get; set; }
        public bool Aktywny { get; set; }
    }
}
