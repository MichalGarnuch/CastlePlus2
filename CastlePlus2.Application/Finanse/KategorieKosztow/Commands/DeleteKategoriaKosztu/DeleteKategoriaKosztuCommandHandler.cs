using CastlePlus2.Application.Interfaces.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Commands.DeleteKategoriaKosztu
{
    public class DeleteKategoriaKosztuCommandHandler : IRequestHandler<DeleteKategoriaKosztuCommand, bool>
    {
        private readonly IKategoriaKosztuRepository _repo;

        public DeleteKategoriaKosztuCommandHandler(IKategoriaKosztuRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteKategoriaKosztuCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdKategoriiKosztu, ct);
            if (entity is null)
                return false;

            await _repo.RemoveAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}