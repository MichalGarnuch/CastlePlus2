using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Domain.Entities.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Commands.CreateWlasnosc
{
    public class CreateWlasnoscCommandHandler : IRequestHandler<CreateWlasnoscCommand, WlasnoscDto>
    {
        private readonly IWlasnoscRepository _repo;
        private readonly IMapper _mapper;

        public CreateWlasnoscCommandHandler(IWlasnoscRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<WlasnoscDto> Handle(CreateWlasnoscCommand command, CancellationToken ct)
        {
            var r = command.Request;

            if (r.IdEncji == Guid.Empty)
                throw new InvalidOperationException("IdEncji jest wymagane.");

            if (r.IdPodmiotu <= 0)
                throw new InvalidOperationException("IdPodmiotu musi być > 0.");

            // Prosta walidacja biznesowa udziału: 0..100 (bez narzucania sumowania po encji – to ewentualnie później)
            if (r.UdzialProcent <= 0 || r.UdzialProcent > 100)
                throw new InvalidOperationException("UdzialProcent musi być w zakresie (0, 100].");

            if (r.DoDnia.HasValue && r.DoDnia.Value < r.OdDnia)
                throw new InvalidOperationException("DoDnia nie może być wcześniejsze niż OdDnia.");

            // Najważniejsze: walidacja FK zanim SQL wywali wyjątek
            if (!await _repo.EncjaExistsAsync(r.IdEncji, ct))
                throw new InvalidOperationException("Nie istnieje Encja o podanym IdEncji.");

            if (!await _repo.PodmiotExistsAsync(r.IdPodmiotu, ct))
                throw new InvalidOperationException("Nie istnieje Podmiot o podanym IdPodmiotu.");

            var entity = new Wlasnosc
            {
                IdEncji = r.IdEncji,
                IdPodmiotu = r.IdPodmiotu,
                UdzialProcent = r.UdzialProcent,
                OdDnia = r.OdDnia,
                DoDnia = r.DoDnia
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<WlasnoscDto>(entity);
        }
    }
}
