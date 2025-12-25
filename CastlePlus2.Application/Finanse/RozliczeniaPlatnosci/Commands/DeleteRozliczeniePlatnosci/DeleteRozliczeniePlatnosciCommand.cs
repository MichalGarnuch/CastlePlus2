using MediatR;

namespace CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Commands.DeleteRozliczeniePlatnosci
{
    public class DeleteRozliczeniePlatnosciCommand : IRequest
    {
        public long IdRozliczenia { get; set; }
    }
}
