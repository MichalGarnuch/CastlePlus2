using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Liczniki.Commands.DeleteLicznik
{
    public class DeleteLicznikCommandHandler : IRequestHandler<DeleteLicznikCommand, bool>
    {
        private readonly ILicznikRepository _repo;

        public DeleteLicznikCommandHandler(ILicznikRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteLicznikCommand request, CancellationToken ct)
        {
            if (request.IdLicznika <= 0)
                return false;

            var entity = await _repo.GetForUpdateAsync(request.IdLicznika, ct);
            if (entity is null)
                return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
