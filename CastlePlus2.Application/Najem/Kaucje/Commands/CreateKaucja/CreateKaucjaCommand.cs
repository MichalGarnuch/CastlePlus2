using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.Kaucje.Commands.CreateKaucja
{
    public class CreateKaucjaCommand : IRequest<KaucjaDto>
    {
        public Guid IdUmowyNajmu { get; set; }
        public string RodzajOperacji { get; set; } = default!;
        public decimal Kwota { get; set; }
        public string KodWaluty { get; set; } = default!;
        public DateOnly DataOperacji { get; set; }
    }
}