using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.CreatePrzedmiotNajmu
{
    public class CreatePrzedmiotNajmuCommand : IRequest<PrzedmiotNajmuDto>
    {
        public Guid IdUmowyNajmu { get; set; }
        public Guid IdEncji { get; set; }
        public decimal? UdzialProcent { get; set; }
        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }
    }
}
