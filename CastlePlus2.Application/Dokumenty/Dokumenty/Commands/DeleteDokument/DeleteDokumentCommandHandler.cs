using CastlePlus2.Application.Interfaces.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.Dokumenty.Commands.DeleteDokument
{
    public class DeleteDokumentCommandHandler : IRequestHandler<DeleteDokumentCommand, bool>
    {
        private readonly IDokumentRepository _repo;

        public DeleteDokumentCommandHandler(IDokumentRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteDokumentCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdDokumentu, ct);
            if (entity is null)
                return false;

            await _repo.RemoveAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
