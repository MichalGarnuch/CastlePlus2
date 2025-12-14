using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.SkladnikiCzynszu.Commands.CreateSkladnikCzynszu
{
    public class CreateSkladnikCzynszuCommand : IRequest<SkladnikCzynszuDto>
    {
        public Guid IdUmowyNajmu { get; set; }
        public string Nazwa { get; set; } = default!;
        public string KodJednostki { get; set; } = default!;
        public decimal Stawka { get; set; }
        public decimal? IloscBazowa { get; set; }
        public string? KodIndeksacji { get; set; }
        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }
    }
}
