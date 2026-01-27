using CastlePlus2.Application.Common.Exceptions;
using CastlePlus2.Application.Interfaces.Konfiguracja;
using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Commands.UpdateZasobUITekst
{
    public class UpdateZasobUITekstCommandHandler : IRequestHandler<UpdateZasobUITekstCommand, bool>
    {
        private readonly IZasobUITekstRepository _repo;

        public UpdateZasobUITekstCommandHandler(IZasobUITekstRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateZasobUITekstCommand request, CancellationToken ct)
        {
            var existing = await _repo.GetForUpdateAsync(request.IdZasobuTekstu, ct);
            if (existing == null) return false;

            var duplicate = await _repo.GetByKeyAsync(request.IdEncji, request.Jezyk, request.Pole, ct);
            if (duplicate != null && duplicate.IdZasobuTekstu != request.IdZasobuTekstu)
                throw new BusinessConflictException("Istnieje już tekst dla podanego języka i pola.");

            existing.IdEncji = request.IdEncji;
            existing.Jezyk = request.Jezyk;
            existing.Pole = request.Pole;
            existing.Wartosc = request.Wartosc;
            existing.Format = string.IsNullOrWhiteSpace(request.Format) ? existing.Format : request.Format;
            existing.Sort = request.Sort;
            existing.ZmienionoUtc = DateTime.UtcNow;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}