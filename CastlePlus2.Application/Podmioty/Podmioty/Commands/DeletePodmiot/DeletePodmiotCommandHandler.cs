using CastlePlus2.Application.Interfaces.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Podmioty.Commands.DeletePodmiot
{
    public class DeletePodmiotCommandHandler : IRequestHandler<DeletePodmiotCommand, bool>
    {
        private readonly IPodmiotRepository _repo;

        public DeletePodmiotCommandHandler(IPodmiotRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeletePodmiotCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(cmd.IdPodmiotu, ct);
            if (entity is null)
                return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}
