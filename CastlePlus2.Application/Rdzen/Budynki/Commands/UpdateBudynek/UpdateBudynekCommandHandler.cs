using CastlePlus2.Application.Interfaces.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Budynki.Commands.UpdateBudynek
{
    public sealed class UpdateBudynekCommandHandler : IRequestHandler<UpdateBudynekCommand, bool>
    {
        private readonly IBudynekRepository _repo;

        public UpdateBudynekCommandHandler(IBudynekRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateBudynekCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.Id, ct);
            if (entity is null) return false;

            entity.IdNieruchomosci = cmd.IdNieruchomosci;
            entity.KodBudynku = cmd.KodBudynku;
            entity.IdAdresu = cmd.IdAdresu;
            entity.Kondygnacje = cmd.Kondygnacje;
            entity.PowierzchniaUzytkowa = cmd.PowierzchniaUzytkowa;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
