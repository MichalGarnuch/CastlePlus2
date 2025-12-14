using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Commands.CreateWlasnosc
{
    public class CreateWlasnoscCommand : IRequest<WlasnoscDto>
    {
        public CreateWlasnoscRequest Request { get; set; } = new();
    }
}
