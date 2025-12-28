using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Platnosci.Commands.UpdatePlatnosc
{
    public class UpdatePlatnoscCommandHandler : IRequestHandler<UpdatePlatnoscCommand, bool>
    {
        private readonly IPlatnoscRepository _repo;

        public UpdatePlatnoscCommandHandler(IPlatnoscRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdatePlatnoscCommand request, CancellationToken ct)
        {
            if (request.IdPlatnosci <= 0)
                return false;

            var entity = await _repo.GetForUpdateAsync(request.IdPlatnosci, ct);
            if (entity is null)
                return false;

            if (request.IdPodmiotu <= 0)
                throw new InvalidOperationException("IdPodmiotu musi być dodatni.");

            if (string.IsNullOrWhiteSpace(request.KodWaluty))
                throw new InvalidOperationException("KodWaluty jest wymagany.");

            if (!await _repo.PodmiotExistsAsync(request.IdPodmiotu, ct))
                throw new InvalidOperationException("Nie istnieje Podmiot dla podanego IdPodmiotu.");

            var kod = request.KodWaluty.Trim().ToUpperInvariant();
            if (!await _repo.WalutaExistsAsync(kod, ct))
                throw new InvalidOperationException("Nie istnieje Waluta dla podanego KodWaluty.");

            entity.IdPodmiotu = request.IdPodmiotu;
            entity.DataPlatnosci = request.DataPlatnosci.Date;
            entity.KodWaluty = kod;
            entity.Kwota = request.Kwota;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}