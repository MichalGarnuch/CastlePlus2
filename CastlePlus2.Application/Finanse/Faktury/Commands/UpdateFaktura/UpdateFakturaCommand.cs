using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Faktury.Commands.UpdateFaktura
{
    public class UpdateFakturaCommand : IRequest<FakturaDto?>
    {
        public long IdFaktury { get; }
        public UpdateFakturaRequest Request { get; }

        public UpdateFakturaCommand(long idFaktury, UpdateFakturaRequest request)
        {
            IdFaktury = idFaktury;
            Request = request;
        }
    }
}
