using CastlePlus2.Application.Interfaces.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.PozycjeKosztow.Commands.DeletePozycjaKosztu
{
    public class DeletePozycjaKosztuCommandHandler : IRequestHandler<DeletePozycjaKosztuCommand>
    {
        private readonly IPozycjaKosztuRepository _repo;

        public DeletePozycjaKosztuCommandHandler(IPozycjaKosztuRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeletePozycjaKosztuCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdPozycjiKosztu, ct);
            if (entity is null)
                return;

            await _repo.RemoveAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
        }
    }
}
