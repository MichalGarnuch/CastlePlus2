using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Budynki.Commands.UpdateBudynek
{
    public sealed class UpdateBudynekCommandHandler : IRequestHandler<UpdateBudynekCommand, BudynekDto?>
    {
        private readonly IBudynekRepository _repo;
        private readonly IMapper _mapper;

        public UpdateBudynekCommandHandler(IBudynekRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<BudynekDto?> Handle(UpdateBudynekCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.Id, ct);
            if (entity is null) return null;

            entity.IdNieruchomosci = cmd.Request.IdNieruchomosci;
            entity.KodBudynku = cmd.Request.KodBudynku;
            entity.IdAdresu = cmd.Request.IdAdresu;
            entity.Kondygnacje = cmd.Request.Kondygnacje;
            entity.PowierzchniaUzytkowa = cmd.Request.PowierzchniaUzytkowa;

            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<BudynekDto>(entity);
        }
    }
}
