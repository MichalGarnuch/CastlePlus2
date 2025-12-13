using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.PozycjeKosztow.Commands.CreatePozycjaKosztu
{
    public class CreatePozycjaKosztuCommand : IRequest<PozycjaKosztuDto>
    {
        public long IdFaktury { get; set; }
        public long IdKategoriiKosztu { get; set; }
        public string? Opis { get; set; }
        public decimal KwotaNetto { get; set; }
        public decimal KwotaBrutto { get; set; }
    }
}
