using CastlePlus2.Application.Common.Exceptions;
using CastlePlus2.Application.Interfaces.Konfiguracja;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUI.Commands.UpdateZasobUI
{
    public class UpdateZasobUICommandHandler : IRequestHandler<UpdateZasobUICommand, bool>
    {
        private readonly IZasobUIRepository _repo;
        private readonly ILogger<UpdateZasobUICommandHandler> _logger;

        public UpdateZasobUICommandHandler(IZasobUIRepository repo, ILogger<UpdateZasobUICommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateZasobUICommand request, CancellationToken ct)
        {
            var existing = await _repo.GetForUpdateAsync(request.IdEncji, ct);
            if (existing == null)
            {
                _logger.LogWarning("Update ZasobUI: nie znaleziono encji IdEncji={IdEncji}.", request.IdEncji);
                return false;
            }

            _logger.LogInformation(
                "Update ZasobUI START: IdEncji={IdEncji}, CurrentCzyAktywny={CurrentCzyAktywny}, RequestCzyAktywny={RequestCzyAktywny}, Typ={Typ}, Kategoria={Kategoria}.",
                request.IdEncji,
                existing.CzyAktywny,
                request.CzyAktywny,
                request.Typ,
                request.Kategoria);

            var duplicate = await _repo.GetByKodZasobuAsync(request.KodZasobu, ct);
            if (duplicate != null && duplicate.IdEncji != request.IdEncji)
            {
                throw new BusinessConflictException("Istnieje już zasób o podanym kodzie.");
            }

            existing.KodZasobu = request.KodZasobu;
            existing.Typ = request.Typ;
            existing.Kategoria = request.Kategoria;
            existing.CzyAktywny = request.CzyAktywny;
            existing.Sort = request.Sort;
            existing.WazneOdUtc = request.WazneOdUtc;
            existing.WazneDoUtc = request.WazneDoUtc;
            existing.ZmienionoUtc = DateTime.UtcNow;

            var rows = await _repo.SaveChangesAsync(ct);

            _logger.LogInformation(
                "Update ZasobUI END: IdEncji={IdEncji}, SavedCzyAktywny={SavedCzyAktywny}, RowsAffected={RowsAffected}.",
                request.IdEncji,
                existing.CzyAktywny,
                rows);

            return true;
        }
    }
}