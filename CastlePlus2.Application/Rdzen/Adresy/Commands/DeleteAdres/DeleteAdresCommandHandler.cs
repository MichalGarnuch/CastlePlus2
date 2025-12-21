using CastlePlus2.Application.Interfaces.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Adresy.Commands.DeleteAdres
{
    public class DeleteAdresCommandHandler : IRequestHandler<DeleteAdresCommand, bool>
    {
        private readonly IAdresRepository _repo;

        public DeleteAdresCommandHandler(IAdresRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteAdresCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.IdAdresu, ct);
            if (entity is null) return false;

            await _repo.RemoveAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
