using CastlePlus2.Application.Interfaces.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.AlokacjeKosztow.Commands.DeleteAlokacjaKosztu
{
    public class DeleteAlokacjaKosztuCommandHandler : IRequestHandler<DeleteAlokacjaKosztuCommand, bool>
    {
        private readonly IAlokacjaKosztuRepository _repo;

        public DeleteAlokacjaKosztuCommandHandler(IAlokacjaKosztuRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteAlokacjaKosztuCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdAlokacji, ct);
            if (entity is null) return false;

            await _repo.RemoveAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
