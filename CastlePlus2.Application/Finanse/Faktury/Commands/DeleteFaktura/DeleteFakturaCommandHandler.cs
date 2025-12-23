using CastlePlus2.Application.Interfaces.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Faktury.Commands.DeleteFaktura
{
    public class DeleteFakturaCommandHandler : IRequestHandler<DeleteFakturaCommand, bool>
    {
        private readonly IFakturaRepository _repo;

        public DeleteFakturaCommandHandler(IFakturaRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteFakturaCommand request, CancellationToken ct)
        {
            if (request.IdFaktury <= 0)
                return false;

            var entity = await _repo.GetForUpdateAsync(request.IdFaktury, ct);
            if (entity is null)
                return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
