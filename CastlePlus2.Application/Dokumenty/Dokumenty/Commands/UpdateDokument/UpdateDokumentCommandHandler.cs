using CastlePlus2.Application.Interfaces.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.Dokumenty.Commands.UpdateDokument
{
    public class UpdateDokumentCommandHandler : IRequestHandler<UpdateDokumentCommand, bool>
    {
        private readonly IDokumentRepository _repo;

        public UpdateDokumentCommandHandler(IDokumentRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateDokumentCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdDokumentu, ct);
            if (entity is null)
                return false;

            // DataUtworzenia zostaje bez zmian (DEFAULT z SQL)
            entity.IdEncjiOwner = request.IdEncjiOwner;
            entity.Nazwa = request.Nazwa;
            entity.Opis = request.Opis;
            entity.SciezkaPliku = request.SciezkaPliku;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
