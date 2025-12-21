using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Commands.UpdatePrzypisanieAdresu
{
    public sealed class UpdatePrzypisanieAdresuCommandHandler
        : IRequestHandler<UpdatePrzypisanieAdresuCommand, PrzypisanieAdresuDto?>
    {
        private readonly IPrzypisanieAdresuRepository _repo;
        private readonly IMapper _mapper;

        public UpdatePrzypisanieAdresuCommandHandler(IPrzypisanieAdresuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PrzypisanieAdresuDto?> Handle(UpdatePrzypisanieAdresuCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.IdPrzypisaniaAdresu, ct);
            if (entity is null) return null;

            entity.IdAdresu = cmd.Request.IdAdresu;
            entity.OdDnia = cmd.Request.OdDnia;
            entity.DoDnia = cmd.Request.DoDnia;

            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<PrzypisanieAdresuDto>(entity);
        }
    }
}
