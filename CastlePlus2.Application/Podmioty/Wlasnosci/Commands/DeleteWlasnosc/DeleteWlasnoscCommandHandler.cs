using CastlePlus2.Application.Interfaces.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Commands.DeleteWlasnosc
{
    public class DeleteWlasnoscCommandHandler : IRequestHandler<DeleteWlasnoscCommand, bool>
    {
        private readonly IWlasnoscRepository _repo;

        public DeleteWlasnoscCommandHandler(IWlasnoscRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteWlasnoscCommand command, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(command.IdWlasnosci, ct);
            if (entity is null)
                return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}