using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Media;
using MediatR;

namespace CastlePlus2.Application.Media.RodzajeMediow.Commands.DeleteRodzajMedium
{
    public sealed class DeleteRodzajMediumCommandHandler : IRequestHandler<DeleteRodzajMediumCommand, bool>
    {
        private readonly IRodzajMediumRepository _repo;

        public DeleteRodzajMediumCommandHandler(IRodzajMediumRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteRodzajMediumCommand request, CancellationToken ct)
        {
            var kod = (request.KodRodzaju ?? string.Empty).Trim();
            if (kod.Length == 0 || kod.Length > 20) return false;

            var entity = await _repo.GetByIdAsync(kod, ct);
            if (entity is null) return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}
