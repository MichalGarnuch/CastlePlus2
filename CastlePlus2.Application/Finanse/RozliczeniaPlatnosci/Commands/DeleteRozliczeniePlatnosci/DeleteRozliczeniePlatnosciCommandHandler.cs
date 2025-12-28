using CastlePlus2.Application.Interfaces.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Commands.DeleteRozliczeniePlatnosci
{
    public class DeleteRozliczeniePlatnosciCommandHandler : IRequestHandler<DeleteRozliczeniePlatnosciCommand, bool>
    {
        private readonly IRozliczeniePlatnosciRepository _repo;

        public DeleteRozliczeniePlatnosciCommandHandler(IRozliczeniePlatnosciRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteRozliczeniePlatnosciCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdRozliczenia, ct);
            if (entity is null)
                return false;

            await _repo.RemoveAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}