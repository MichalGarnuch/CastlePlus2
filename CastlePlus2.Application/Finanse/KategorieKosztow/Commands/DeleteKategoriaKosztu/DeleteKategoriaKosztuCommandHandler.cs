using CastlePlus2.Application.Interfaces.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Commands.DeleteKategoriaKosztu
{
    public class DeleteKategoriaKosztuCommandHandler : IRequestHandler<DeleteKategoriaKosztuCommand>
    {
        private readonly IKategoriaKosztuRepository _repo;

        public DeleteKategoriaKosztuCommandHandler(IKategoriaKosztuRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeleteKategoriaKosztuCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdKategoriiKosztu, ct);
            if (entity is null)
                return;

            await _repo.RemoveAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
        }
    }
}
