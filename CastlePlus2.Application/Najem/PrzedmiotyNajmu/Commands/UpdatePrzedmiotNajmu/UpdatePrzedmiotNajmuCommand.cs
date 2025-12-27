using System;
using MediatR;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.UpdatePrzedmiotNajmu
{
    public class UpdatePrzedmiotNajmuCommand : IRequest<bool>
    {
        public long IdPrzedmiotuNajmu { get; set; }
        public Guid IdUmowyNajmu { get; set; }
        public Guid IdEncji { get; set; }
        public decimal? UdzialProcent { get; set; }
        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }
    }
}