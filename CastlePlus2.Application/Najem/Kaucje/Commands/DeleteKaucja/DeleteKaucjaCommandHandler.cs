using CastlePlus2.Application.Interfaces.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.Kaucje.Commands.DeleteKaucja
{
    public class DeleteKaucjaCommandHandler : IRequestHandler<DeleteKaucjaCommand, bool>
    {
        private readonly IKaucjaRepository _repo;

        public DeleteKaucjaCommandHandler(IKaucjaRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteKaucjaCommand request, CancellationToken ct)
        {
            if (request.IdKaucji <= 0)
                return false;

            var entity = await _repo.GetForUpdateAsync(request.IdKaucji, ct);
            if (entity is null)
                return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}