using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Przylacza.Commands.DeletePrzylacze
{
    public sealed class DeletePrzylaczeCommandHandler
        : IRequestHandler<DeletePrzylaczeCommand, bool>
    {
        private readonly IPrzylaczeRepository _repo;

        public DeletePrzylaczeCommandHandler(IPrzylaczeRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeletePrzylaczeCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.IdPrzylacza, ct);
            if (entity is null) return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
