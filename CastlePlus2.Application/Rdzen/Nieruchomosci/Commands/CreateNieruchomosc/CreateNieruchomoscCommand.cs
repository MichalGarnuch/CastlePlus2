using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.CreateNieruchomosc
{
    public class CreateNieruchomoscCommand : IRequest<NieruchomoscDto>
    {
        public CreateNieruchomoscRequest Request { get; set; } = new();
    }
}
