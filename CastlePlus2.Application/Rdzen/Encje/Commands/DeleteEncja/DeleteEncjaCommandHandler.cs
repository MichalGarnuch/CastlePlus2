using CastlePlus2.Application.Interfaces.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Encje.Commands.DeleteEncja
{
    public class DeleteEncjaCommandHandler : IRequestHandler<DeleteEncjaCommand, bool>
    {
        private readonly IEncjaRepository _repo;

        public DeleteEncjaCommandHandler(IEncjaRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteEncjaCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.Id, ct);
            if (entity is null)
            {
                return false;
            }

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}