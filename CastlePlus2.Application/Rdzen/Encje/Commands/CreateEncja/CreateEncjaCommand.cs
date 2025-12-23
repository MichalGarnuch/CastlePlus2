using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Encje.Commands.CreateEncja
{
    public class CreateEncjaCommand : IRequest<EncjaDto>
    {
        public CreateEncjaRequest Request { get; set; } = new();
    }
}