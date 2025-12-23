using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Kontakty.Commands.DeleteKontakt
{
    public class DeleteKontaktCommandHandler : IRequestHandler<DeleteKontaktCommand, bool>
    {
        private readonly IKontaktRepository _repo;

        public DeleteKontaktCommandHandler(IKontaktRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteKontaktCommand command, CancellationToken ct)
        {
            var entity = await _repo.GetByIdForUpdateAsync(command.IdKontaktu, ct);
            if (entity is null)
                return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}