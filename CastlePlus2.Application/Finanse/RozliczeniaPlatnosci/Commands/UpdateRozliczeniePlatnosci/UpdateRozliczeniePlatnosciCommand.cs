using MediatR;

namespace CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Commands.UpdateRozliczeniePlatnosci
{
    public class UpdateRozliczeniePlatnosciCommand : IRequest<bool>
    {
        public long IdRozliczenia { get; set; }
        public long IdPlatnosci { get; set; }
        public long IdFaktury { get; set; }
        public decimal Kwota { get; set; }
    }
}