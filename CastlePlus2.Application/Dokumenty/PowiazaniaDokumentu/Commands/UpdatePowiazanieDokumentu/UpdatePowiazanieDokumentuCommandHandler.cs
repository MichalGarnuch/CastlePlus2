using System;
using CastlePlus2.Application.Interfaces.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Commands.UpdatePowiazanieDokumentu
{
    public class UpdatePowiazanieDokumentuCommandHandler
        : IRequestHandler<UpdatePowiazanieDokumentuCommand, bool>
    {
        private readonly IPowiazanieDokumentuRepository _powiazanieRepo;
        private readonly IDokumentRepository _dokumentRepo;

        public UpdatePowiazanieDokumentuCommandHandler(
            IPowiazanieDokumentuRepository powiazanieRepo,
            IDokumentRepository dokumentRepo)
        {
            _powiazanieRepo = powiazanieRepo;
            _dokumentRepo = dokumentRepo;
        }

        public async Task<bool> Handle(UpdatePowiazanieDokumentuCommand request, CancellationToken ct)
        {
            if (request.IdPowiazania <= 0)
                throw new InvalidOperationException("IdPowiazania musi by > 0.");

            if (request.IdDokumentu <= 0)
                throw new InvalidOperationException("IdDokumentu musi by > 0.");

            if (request.IdEncji == Guid.Empty)
                throw new InvalidOperationException("IdEncji nie może być pustym GUID.");

            var entity = await _powiazanieRepo.GetForUpdateAsync(request.IdPowiazania, ct);
            if (entity is null)
                return false;

            // Czytelny błąd zanim poleci FK z bazy:
            var dokument = await _dokumentRepo.GetByIdAsync(request.IdDokumentu, ct);
            if (dokument is null)
                throw new InvalidOperationException($"Dokument o IdDokumentu={request.IdDokumentu} nie istnieje.");

            var existsOther = await _powiazanieRepo.ExistsOtherAsync(request.IdDokumentu, request.IdEncji, request.IdPowiazania, ct);
            if (existsOther)
                throw new InvalidOperationException("Takie powiązanie już istnieje (IdDokumentu + IdEncji).");

            entity.IdDokumentu = request.IdDokumentu;
            entity.IdEncji = request.IdEncji;

            await _powiazanieRepo.SaveChangesAsync(ct);
            return true;
        }
    }
}
