using System;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.ProcesyRdzen.Commands.PrzypiszAdres
{
    public sealed class PrzypiszAdresCommandHandler
        : IRequestHandler<PrzypiszAdresCommand, PrzypiszAdresResultDto>
    {
        private readonly IPrzypisanieAdresuRepository _repo;

        public PrzypiszAdresCommandHandler(IPrzypisanieAdresuRepository repo)
        {
            _repo = repo;
        }

        public async Task<PrzypiszAdresResultDto> Handle(PrzypiszAdresCommand request, CancellationToken ct)
        {
            var hasOverlap = await _repo.ExistsOverlapAsync(request.IdEncji, request.OdDnia, request.DoDnia, ct);
            if (hasOverlap)
            {
                throw new InvalidOperationException("Istnieje już przypisanie adresu w podanym okresie dla wybranej encji.");
            }

            var entity = new PrzypisanieAdresu
            {
                IdEncji = request.IdEncji,
                IdAdresu = request.IdAdresu,
                OdDnia = request.OdDnia,
                DoDnia = request.DoDnia
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return new PrzypiszAdresResultDto
            {
                IdPrzypisaniaAdresu = entity.IdPrzypisaniaAdresu,
                IdEncji = entity.IdEncji,
                IdAdresu = entity.IdAdresu,
                OdDnia = entity.OdDnia,
                DoDnia = entity.DoDnia
            };

        }
    }
}