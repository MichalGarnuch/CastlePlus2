using MediatR;

namespace CastlePlus2.Application.Najem.Kaucje.Commands.UpdateKaucja
{
    public class UpdateKaucjaCommand : IRequest<bool>
    {
        public long IdOperacjiKaucji { get; set; }
        public Guid IdUmowyNajmu { get; set; }
        public string RodzajOperacji { get; set; } = default!;
        public decimal Kwota { get; set; }
        public string KodWaluty { get; set; } = default!;
        public DateOnly DataOperacji { get; set; }
    }
}