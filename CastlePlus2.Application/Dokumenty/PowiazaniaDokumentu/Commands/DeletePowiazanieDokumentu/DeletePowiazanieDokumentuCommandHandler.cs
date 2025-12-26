using CastlePlus2.Application.Interfaces.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Commands.DeletePowiazanieDokumentu
{
    public class DeletePowiazanieDokumentuCommandHandler
        : IRequestHandler<DeletePowiazanieDokumentuCommand, bool>
    {
        private readonly IPowiazanieDokumentuRepository _repo;

        public DeletePowiazanieDokumentuCommandHandler(IPowiazanieDokumentuRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeletePowiazanieDokumentuCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdPowiazania, ct);
            if (entity is null)
                return false;

            await _repo.RemoveAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
