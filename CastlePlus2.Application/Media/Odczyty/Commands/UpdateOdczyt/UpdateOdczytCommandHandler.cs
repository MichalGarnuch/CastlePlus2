using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Odczyty.Commands.UpdateOdczyt
{
    public class UpdateOdczytCommandHandler : IRequestHandler<UpdateOdczytCommand, OdczytDto?>
    {
        private readonly IOdczytRepository _repo;
        private readonly IMapper _mapper;

        public UpdateOdczytCommandHandler(IOdczytRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OdczytDto?> Handle(UpdateOdczytCommand request, CancellationToken ct)
        {
            request.Request.Zrodlo = request.Request.Zrodlo?.Trim();

            if (request.IdOdczytu <= 0)
                throw new InvalidOperationException("IdOdczytu musi być > 0.");

            if (request.Request.IdLicznika <= 0)
                throw new InvalidOperationException("IdLicznika musi być > 0.");

            var data = request.Request.DataOdczytu.Date;

            if (request.Request.Zrodlo is not null && request.Request.Zrodlo.Length > 20)
                throw new InvalidOperationException("Zrodlo max 20 znaków.");

            var entity = await _repo.GetForUpdateAsync(request.IdOdczytu, ct);
            if (entity is null)
                return null;

            // FK -> Licznik
            if (!await _repo.LicznikExistsAsync(request.Request.IdLicznika, ct))
                throw new InvalidOperationException("Nie istnieje Licznik o podanym IdLicznika.");

            // UX -> (IdLicznika, DataOdczytu) unikalne (sprawdzamy tylko gdy zmiana klucza UX)
            if (entity.IdLicznika != request.Request.IdLicznika || entity.DataOdczytu.Date != data)
            {
                if (await _repo.ExistsForLicznikAndDateAsync(request.Request.IdLicznika, data, ct))
                    throw new InvalidOperationException("Istnieje odczyt dla tego licznika w tej dacie (unikalne: IdLicznika+DataOdczytu).");
            }

            entity.IdLicznika = request.Request.IdLicznika;
            entity.DataOdczytu = data;
            entity.Wskazanie = request.Request.Wskazanie;
            entity.Zrodlo = request.Request.Zrodlo;

            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<OdczytDto>(entity);
        }
    }
}
