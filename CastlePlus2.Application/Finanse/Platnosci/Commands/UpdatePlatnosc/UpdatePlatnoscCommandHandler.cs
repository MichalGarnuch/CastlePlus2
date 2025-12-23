using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Platnosci.Commands.UpdatePlatnosc
{
    public class UpdatePlatnoscCommandHandler : IRequestHandler<UpdatePlatnoscCommand, PlatnoscDto?>
    {
        private readonly IPlatnoscRepository _repo;
        private readonly IMapper _mapper;

        public UpdatePlatnoscCommandHandler(IPlatnoscRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PlatnoscDto?> Handle(UpdatePlatnoscCommand request, CancellationToken ct)
        {
            if (request.IdPlatnosci <= 0)
                return null;

            var req = request.Request;

            var entity = await _repo.GetForUpdateAsync(request.IdPlatnosci, ct);
            if (entity is null)
                return null;

            if (req.IdPodmiotu <= 0)
                throw new InvalidOperationException("IdPodmiotu musi być dodatni.");

            if (string.IsNullOrWhiteSpace(req.KodWaluty))
                throw new InvalidOperationException("KodWaluty jest wymagany.");

            if (!await _repo.PodmiotExistsAsync(req.IdPodmiotu, ct))
                throw new InvalidOperationException("Nie istnieje Podmiot dla podanego IdPodmiotu.");

            var kod = req.KodWaluty.Trim().ToUpperInvariant();
            if (!await _repo.WalutaExistsAsync(kod, ct))
                throw new InvalidOperationException("Nie istnieje Waluta dla podanego KodWaluty.");

            entity.IdPodmiotu = req.IdPodmiotu;
            entity.DataPlatnosci = req.DataPlatnosci.Date;
            entity.KodWaluty = kod;
            entity.Kwota = req.Kwota;

            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<PlatnoscDto>(entity);
        }
    }
}
