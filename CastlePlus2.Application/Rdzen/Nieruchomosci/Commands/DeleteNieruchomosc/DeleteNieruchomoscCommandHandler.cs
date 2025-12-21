using CastlePlus2.Application.Interfaces.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.DeleteNieruchomosc
{
    public sealed class DeleteNieruchomoscCommandHandler : IRequestHandler<DeleteNieruchomoscCommand, bool>
    {
        private readonly INieruchomoscRepository _repo;

        public DeleteNieruchomoscCommandHandler(INieruchomoscRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteNieruchomoscCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.Id, ct);
            if (entity is null) return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
