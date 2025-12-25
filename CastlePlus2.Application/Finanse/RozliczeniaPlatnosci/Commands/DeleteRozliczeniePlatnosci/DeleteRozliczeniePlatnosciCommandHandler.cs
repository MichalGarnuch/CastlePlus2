using CastlePlus2.Application.Interfaces.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Commands.DeleteRozliczeniePlatnosci
{
    public class DeleteRozliczeniePlatnosciCommandHandler : IRequestHandler<DeleteRozliczeniePlatnosciCommand>
    {
        private readonly IRozliczeniePlatnosciRepository _repo;

        public DeleteRozliczeniePlatnosciCommandHandler(IRozliczeniePlatnosciRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeleteRozliczeniePlatnosciCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdRozliczenia, ct);
            if (entity is null)
                return;

            await _repo.RemoveAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
        }
    }
}
