using CastlePlus2.Application.Interfaces.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Commands.UpdatePrzypisanieAdresu
{
    public sealed class UpdatePrzypisanieAdresuCommandHandler
        : IRequestHandler<UpdatePrzypisanieAdresuCommand, bool>
    {
        private readonly IPrzypisanieAdresuRepository _repo;

        public UpdatePrzypisanieAdresuCommandHandler(IPrzypisanieAdresuRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdatePrzypisanieAdresuCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.IdPrzypisaniaAdresu, ct);
            if (entity is null) return false;

            entity.IdEncji = cmd.IdEncji;
            entity.IdAdresu = cmd.IdAdresu;
            entity.OdDnia = cmd.OdDnia;
            entity.DoDnia = cmd.DoDnia;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}