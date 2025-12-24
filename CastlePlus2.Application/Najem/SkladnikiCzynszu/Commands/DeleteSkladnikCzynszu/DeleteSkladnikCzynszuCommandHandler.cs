using CastlePlus2.Application.Interfaces.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.SkladnikiCzynszu.Commands.DeleteSkladnikCzynszu
{
    public class DeleteSkladnikCzynszuCommandHandler : IRequestHandler<DeleteSkladnikCzynszuCommand, bool>
    {
        private readonly ISkladnikCzynszuRepository _repo;

        public DeleteSkladnikCzynszuCommandHandler(ISkladnikCzynszuRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteSkladnikCzynszuCommand request, CancellationToken ct)
        {
            if (request.IdSkladnikaCzynszu <= 0)
            {
                return false;
            }

            var entity = await _repo.GetByIdAsync(request.IdSkladnikaCzynszu, ct);
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