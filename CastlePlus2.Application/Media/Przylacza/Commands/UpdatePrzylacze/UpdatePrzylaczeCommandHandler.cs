using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Przylacza.Commands.UpdatePrzylacze
{
    public sealed class UpdatePrzylaczeCommandHandler : IRequestHandler<UpdatePrzylaczeCommand, bool>
    {
        private readonly IPrzylaczeRepository _repo;

        public UpdatePrzylaczeCommandHandler(IPrzylaczeRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdatePrzylaczeCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.IdPrzylacza, ct);
            if (entity is null) return false;

            // walidacja FK do Encji (repo już ma EncjaExistsAsync)
            var encjaOk = await _repo.EncjaExistsAsync(cmd.IdEncjiGospodarza, ct);
            if (!encjaOk) return false;

            entity.IdEncjiGospodarza = cmd.IdEncjiGospodarza;
            entity.KodRodzaju = cmd.KodRodzaju;
            entity.Opis = cmd.Opis;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}