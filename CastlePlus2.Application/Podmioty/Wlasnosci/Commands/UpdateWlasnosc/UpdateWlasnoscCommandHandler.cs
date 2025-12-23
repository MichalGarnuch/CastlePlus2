using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Commands.UpdateWlasnosc
{
    public class UpdateWlasnoscCommandHandler : IRequestHandler<UpdateWlasnoscCommand, WlasnoscDto?>
    {
        private readonly IWlasnoscRepository _repo;
        private readonly IMapper _mapper;

        public UpdateWlasnoscCommandHandler(IWlasnoscRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<WlasnoscDto?> Handle(UpdateWlasnoscCommand command, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(command.IdWlasnosci, ct);
            if (entity is null)
                return null;

            var r = command.Request;

            if (r.IdEncji == Guid.Empty)
                throw new InvalidOperationException("IdEncji jest wymagane.");

            if (r.IdPodmiotu <= 0)
                throw new InvalidOperationException("IdPodmiotu musi być > 0.");

            if (r.UdzialProcent <= 0 || r.UdzialProcent > 100)
                throw new InvalidOperationException("UdzialProcent musi być w zakresie (0, 100].");

            if (r.DoDnia.HasValue && r.DoDnia.Value < r.OdDnia)
                throw new InvalidOperationException("DoDnia nie może być wcześniejsze niż OdDnia.");

            if (!await _repo.EncjaExistsAsync(r.IdEncji, ct))
                throw new InvalidOperationException("Nie istnieje Encja o podanym IdEncji.");

            if (!await _repo.PodmiotExistsAsync(r.IdPodmiotu, ct))
                throw new InvalidOperationException("Nie istnieje Podmiot o podanym IdPodmiotu.");

            entity.IdEncji = r.IdEncji;
            entity.IdPodmiotu = r.IdPodmiotu;
            entity.UdzialProcent = r.UdzialProcent;
            entity.OdDnia = r.OdDnia;
            entity.DoDnia = r.DoDnia;

            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<WlasnoscDto>(entity);
        }
    }
}