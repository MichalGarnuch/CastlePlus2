using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.Kaucje.Commands.CreateKaucja
{
    public class CreateKaucjaCommand : IRequest<KaucjaDto>
    {
        public Guid IdUmowyNajmu { get; set; }
        public decimal Kwota { get; set; }
        public string KodWaluty { get; set; } = default!;
        public DateOnly DataWplaty { get; set; }
        public DateOnly? DataZwrotu { get; set; }
        public string Status { get; set; } = default!;
    }
}
