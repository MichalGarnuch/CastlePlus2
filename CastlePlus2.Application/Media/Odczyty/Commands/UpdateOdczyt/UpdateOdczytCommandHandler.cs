using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Odczyty.Commands.UpdateOdczyt
{
    public class UpdateOdczytCommandHandler : IRequestHandler<UpdateOdczytCommand, bool>
    {
        private readonly IOdczytRepository _repo;

        public UpdateOdczytCommandHandler(IOdczytRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateOdczytCommand request, CancellationToken ct)
        {
            request.Zrodlo = request.Zrodlo?.Trim();

            if (request.IdOdczytu <= 0)
                throw new InvalidOperationException("IdOdczytu musi być > 0.");

            if (request.IdLicznika <= 0)
                throw new InvalidOperationException("IdLicznika musi być > 0.");

            var data = request.DataOdczytu.Date;

            if (request.Zrodlo is not null && request.Zrodlo.Length > 20)
                throw new InvalidOperationException("Zrodlo max 20 znaków.");

            var entity = await _repo.GetForUpdateAsync(request.IdOdczytu, ct);
            if (entity is null)
                return false;

            // FK -> Licznik
            if (!await _repo.LicznikExistsAsync(request.IdLicznika, ct))
                throw new InvalidOperationException("Nie istnieje Licznik o podanym IdLicznika.");

            // UX -> (IdLicznika, DataOdczytu) unikalne (sprawdzamy tylko gdy zmiana klucza UX)
            if (entity.IdLicznika != request.IdLicznika || entity.DataOdczytu.Date != data)
            {
                if (await _repo.ExistsForLicznikAndDateAsync(request.IdLicznika, data, ct))
                    throw new InvalidOperationException("Istnieje odczyt dla tego licznika w tej dacie (unikalne: IdLicznika+DataOdczytu).");
            }

            entity.IdLicznika = request.IdLicznika;
            entity.DataOdczytu = data;
            entity.Wskazanie = request.Wskazanie;
            entity.Zrodlo = request.Zrodlo;

            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}