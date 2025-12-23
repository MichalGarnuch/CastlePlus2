using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Encje.Commands.UpdateEncja
{
    public class UpdateEncjaCommand : IRequest<EncjaDto?>
    {
        public Guid Id { get; }
        public UpdateEncjaRequest Request { get; }

        public UpdateEncjaCommand(Guid id, UpdateEncjaRequest request)
        {
            Id = id;
            Request = request;
        }
    }
}