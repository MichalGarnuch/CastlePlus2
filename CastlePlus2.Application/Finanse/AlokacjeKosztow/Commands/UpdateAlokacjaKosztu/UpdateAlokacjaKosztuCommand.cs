using System;
using MediatR;

namespace CastlePlus2.Application.Finanse.AlokacjeKosztow.Commands.UpdateAlokacjaKosztu
{
    public class UpdateAlokacjaKosztuCommand : IRequest<bool>
    {
        public long IdAlokacji { get; set; }

        public long IdPozycjiKosztu { get; set; }
        public Guid IdEncji { get; set; }

        public decimal KwotaNetto { get; set; }
        public decimal KwotaBrutto { get; set; }
    }
}
