using CastlePlus2.Application.Interfaces.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Lokale.Commands.UpdateLokal
{
    public sealed class UpdateLokalCommandHandler : IRequestHandler<UpdateLokalCommand, bool>
    {
        private readonly ILokalRepository _repo;

        public UpdateLokalCommandHandler(ILokalRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateLokalCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.Id, ct);
            if (entity is null) return false;

            entity.IdBudynku = cmd.IdBudynku;
            entity.KodLokalu = cmd.KodLokalu;
            entity.Powierzchnia = cmd.Powierzchnia;
            entity.Przeznaczenie = cmd.Przeznaczenie;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}