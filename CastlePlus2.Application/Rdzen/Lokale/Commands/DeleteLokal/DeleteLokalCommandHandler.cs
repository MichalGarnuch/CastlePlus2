using CastlePlus2.Application.Interfaces.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Lokale.Commands.DeleteLokal
{
    public sealed class DeleteLokalCommandHandler : IRequestHandler<DeleteLokalCommand, bool>
    {
        private readonly ILokalRepository _repo;

        public DeleteLokalCommandHandler(ILokalRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteLokalCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.Id, ct);
            if (entity is null) return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
