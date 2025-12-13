using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Commands.CreateRozliczeniePlatnosci
{
    public class CreateRozliczeniePlatnosciCommand : IRequest<RozliczeniePlatnosciDto>
    {
        public long IdPlatnosci { get; set; }
        public long IdFaktury { get; set; }
        public decimal Kwota { get; set; }
    }
}
