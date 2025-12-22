using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Przylacza.Commands.UpdatePrzylacze
{
    public sealed class UpdatePrzylaczeCommandHandler : IRequestHandler<UpdatePrzylaczeCommand, PrzylaczeDto?>
    {
        private readonly IPrzylaczeRepository _repo;
        private readonly IMapper _mapper;

        public UpdatePrzylaczeCommandHandler(IPrzylaczeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PrzylaczeDto?> Handle(UpdatePrzylaczeCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.IdPrzylacza, ct);
            if (entity is null) return null;

            // walidacja FK do Encji (repo już ma EncjaExistsAsync)
            var encjaOk = await _repo.EncjaExistsAsync(cmd.Request.IdEncjiGospodarza, ct);
            if (!encjaOk) return null; // prosto: zwróć null -> kontroler da 404/400 wg Twojej decyzji

            entity.IdEncjiGospodarza = cmd.Request.IdEncjiGospodarza;
            entity.KodRodzaju = cmd.Request.KodRodzaju;
            entity.Opis = cmd.Request.Opis;

            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<PrzylaczeDto>(entity);
        }
    }
}
