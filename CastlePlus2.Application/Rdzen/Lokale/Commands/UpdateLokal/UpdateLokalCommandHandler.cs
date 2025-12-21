using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Lokale.Commands.UpdateLokal
{
    public sealed class UpdateLokalCommandHandler : IRequestHandler<UpdateLokalCommand, LokalDto?>
    {
        private readonly ILokalRepository _repo;
        private readonly IMapper _mapper;

        public UpdateLokalCommandHandler(ILokalRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<LokalDto?> Handle(UpdateLokalCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.Id, ct);
            if (entity is null) return null;

            entity.IdBudynku = cmd.Request.IdBudynku;
            entity.KodLokalu = cmd.Request.KodLokalu;
            entity.Powierzchnia = cmd.Request.Powierzchnia;
            entity.Przeznaczenie = cmd.Request.Przeznaczenie;

            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<LokalDto>(entity);
        }
    }
}
