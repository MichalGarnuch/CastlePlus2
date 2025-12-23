using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Platnosci.Commands.UpdatePlatnosc
{
    public class UpdatePlatnoscCommand : IRequest<PlatnoscDto?>
    {
        public long IdPlatnosci { get; }
        public UpdatePlatnoscRequest Request { get; }

        public UpdatePlatnoscCommand(long idPlatnosci, UpdatePlatnoscRequest request)
        {
            IdPlatnosci = idPlatnosci;
            Request = request;
        }
    }
}
