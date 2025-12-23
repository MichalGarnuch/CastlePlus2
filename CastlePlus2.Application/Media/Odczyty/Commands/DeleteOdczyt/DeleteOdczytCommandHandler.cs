using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Odczyty.Commands.DeleteOdczyt
{
    public class DeleteOdczytCommandHandler : IRequestHandler<DeleteOdczytCommand, bool>
    {
        private readonly IOdczytRepository _repo;

        public DeleteOdczytCommandHandler(IOdczytRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteOdczytCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdOdczytu, ct);
            if (entity is null)
                return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}
