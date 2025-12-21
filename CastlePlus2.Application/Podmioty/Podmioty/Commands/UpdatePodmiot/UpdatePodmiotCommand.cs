using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.Requests.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Podmioty.Commands.UpdatePodmiot
{
    public class UpdatePodmiotCommand : IRequest<PodmiotDto?>
    {
        public long IdPodmiotu { get; set; }
        public UpdatePodmiotRequest Request { get; set; } = default!;
    }
}
