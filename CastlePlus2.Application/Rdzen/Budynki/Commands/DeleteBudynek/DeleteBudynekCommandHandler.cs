using CastlePlus2.Application.Interfaces.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Budynki.Commands.DeleteBudynek
{
    public sealed class DeleteBudynekCommandHandler : IRequestHandler<DeleteBudynekCommand, bool>
    {
        private readonly IBudynekRepository _repo;

        public DeleteBudynekCommandHandler(IBudynekRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteBudynekCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.Id, ct);
            if (entity is null) return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
