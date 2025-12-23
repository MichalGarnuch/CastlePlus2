using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Platnosci.Commands.DeletePlatnosc
{
    public class DeletePlatnoscCommandHandler : IRequestHandler<DeletePlatnoscCommand, bool>
    {
        private readonly IPlatnoscRepository _repo;

        public DeletePlatnoscCommandHandler(IPlatnoscRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeletePlatnoscCommand request, CancellationToken ct)
        {
            if (request.IdPlatnosci <= 0)
                return false;

            var entity = await _repo.GetForUpdateAsync(request.IdPlatnosci, ct);
            if (entity is null)
                return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
