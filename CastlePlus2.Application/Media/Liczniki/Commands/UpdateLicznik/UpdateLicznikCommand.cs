using CastlePlus2.Contracts.DTOs.Media;
using CastlePlus2.Contracts.Requests.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Liczniki.Commands.UpdateLicznik
{
    public class UpdateLicznikCommand : IRequest<LicznikDto?>
    {
        public long IdLicznika { get; }
        public UpdateLicznikRequest Request { get; }

        public UpdateLicznikCommand(long idLicznika, UpdateLicznikRequest request)
        {
            IdLicznika = idLicznika;
            Request = request;
        }
    }
}
