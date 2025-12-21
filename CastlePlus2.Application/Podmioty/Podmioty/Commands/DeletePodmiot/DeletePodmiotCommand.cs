using MediatR;

namespace CastlePlus2.Application.Podmioty.Podmioty.Commands.DeletePodmiot
{
    public class DeletePodmiotCommand : IRequest<bool>
    {
        public long IdPodmiotu { get; }
        public DeletePodmiotCommand(long idPodmiotu) => IdPodmiotu = idPodmiotu;
    }
}
