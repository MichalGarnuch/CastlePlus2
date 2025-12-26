using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Commands.DeletePowiazanieZlecenia
{
    public sealed class DeletePowiazanieZleceniaCommandHandler : IRequestHandler<DeletePowiazanieZleceniaCommand, bool>
    {
        private readonly IPowiazanieZleceniaRepository _repo;

        public DeletePowiazanieZleceniaCommandHandler(IPowiazanieZleceniaRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeletePowiazanieZleceniaCommand request, CancellationToken ct)
        {
            if (request.IdPowiazania <= 0)
                return false;

            var entity = await _repo.GetByIdAsync(request.IdPowiazania, ct);
            if (entity is null)
                return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}