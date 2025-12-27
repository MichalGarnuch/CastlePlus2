using CastlePlus2.Application.Interfaces.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.UpdateNieruchomosc
{
    public sealed class UpdateNieruchomoscCommandHandler : IRequestHandler<UpdateNieruchomoscCommand, bool>
    {
        private readonly INieruchomoscRepository _repo;

        public UpdateNieruchomoscCommandHandler(INieruchomoscRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateNieruchomoscCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.Id, ct);
            if (entity is null) return false;

            entity.Nazwa = cmd.Nazwa;
            entity.IdAdresuGlownego = cmd.IdAdresuGlownego;
            entity.IdPodmiotuWlasciciela = cmd.IdPodmiotuWlasciciela;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
