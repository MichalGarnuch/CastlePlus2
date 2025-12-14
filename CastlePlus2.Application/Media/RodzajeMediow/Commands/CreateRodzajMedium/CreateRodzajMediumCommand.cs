using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.RodzajeMediow.Commands.CreateRodzajMedium
{
    public class CreateRodzajMediumCommand : IRequest<RodzajMediumDto>
    {
        public string KodRodzaju { get; set; } = string.Empty;
        public string Nazwa { get; set; } = string.Empty;
    }
}
