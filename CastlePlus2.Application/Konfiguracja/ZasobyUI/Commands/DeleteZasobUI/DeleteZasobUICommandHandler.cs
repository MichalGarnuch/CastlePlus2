using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Application.Interfaces.Konfiguracja;
using CastlePlus2.Application.Interfaces.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUI.Commands.DeleteZasobUI
{
    public class DeleteZasobUICommandHandler : IRequestHandler<DeleteZasobUICommand, bool>
    {
        private readonly IZasobUIRepository _repo;
        private readonly IZasobUITekstRepository _tekstRepo;
        private readonly IPowiazanieDokumentuRepository _powiazanieRepo;
        private readonly IEncjaRepository _encjaRepo;

        public DeleteZasobUICommandHandler(
            IZasobUIRepository repo,
            IZasobUITekstRepository tekstRepo,
            IPowiazanieDokumentuRepository powiazanieRepo,
            IEncjaRepository encjaRepo)
        {
            _repo = repo;
            _tekstRepo = tekstRepo;
            _powiazanieRepo = powiazanieRepo;
            _encjaRepo = encjaRepo;
        }

        public async Task<bool> Handle(DeleteZasobUICommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdEncji, ct);
            if (entity == null)
            {
                return false;
            }

            var powiazania = await _powiazanieRepo.GetByEncjaIdAsync(request.IdEncji, ct);
            foreach (var powiazanie in powiazania)
            {
                await _powiazanieRepo.RemoveAsync(powiazanie, ct);
            }

            var teksty = await _tekstRepo.GetByEncjaIdAsync(request.IdEncji, ct);
            foreach (var tekst in teksty)
            {
                _tekstRepo.Remove(tekst);
            }

            _repo.Remove(entity);

            var encja = await _encjaRepo.GetForUpdateAsync(request.IdEncji, ct);
            if (encja != null)
            {
                _encjaRepo.Remove(encja);
            }

            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}