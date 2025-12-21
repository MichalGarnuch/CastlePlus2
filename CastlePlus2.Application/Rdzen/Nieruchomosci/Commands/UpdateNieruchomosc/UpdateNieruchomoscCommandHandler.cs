using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.UpdateNieruchomosc
{
    public sealed class UpdateNieruchomoscCommandHandler : IRequestHandler<UpdateNieruchomoscCommand, NieruchomoscDto?>
    {
        private readonly INieruchomoscRepository _repo;
        private readonly IMapper _mapper;

        public UpdateNieruchomoscCommandHandler(INieruchomoscRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<NieruchomoscDto?> Handle(UpdateNieruchomoscCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.Id, ct);
            if (entity is null) return null;

            entity.Nazwa = cmd.Request.Nazwa;
            entity.IdAdresuGlownego = cmd.Request.IdAdresuGlownego;
            entity.IdPodmiotuWlasciciela = cmd.Request.IdPodmiotuWlasciciela;

            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<NieruchomoscDto>(entity);
        }
    }
}
