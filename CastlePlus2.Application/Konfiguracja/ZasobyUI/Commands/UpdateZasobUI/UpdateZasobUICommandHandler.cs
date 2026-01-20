using CastlePlus2.Application.Common.Exceptions;
using CastlePlus2.Application.Interfaces.Konfiguracja;
using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUI.Commands.UpdateZasobUI
{
    public class UpdateZasobUICommandHandler : IRequestHandler<UpdateZasobUICommand, bool>
    {
        private readonly IZasobUIRepository _repo;

        public UpdateZasobUICommandHandler(IZasobUIRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateZasobUICommand request, CancellationToken ct)
        {
            var existing = await _repo.GetForUpdateAsync(request.IdEncji, ct);
            if (existing == null)
            {
                return false;
            }

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

            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}