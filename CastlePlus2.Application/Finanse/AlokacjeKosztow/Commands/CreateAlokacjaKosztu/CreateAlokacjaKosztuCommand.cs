using System;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.AlokacjeKosztow.Commands.CreateAlokacjaKosztu
{
    public class CreateAlokacjaKosztuCommand : IRequest<AlokacjaKosztuDto>
    {
        public long IdPozycjiKosztu { get; set; }
        public Guid IdEncji { get; set; }
        public decimal KwotaNetto { get; set; }
        public decimal KwotaBrutto { get; set; }
    }
}
