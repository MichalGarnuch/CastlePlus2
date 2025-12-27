using CastlePlus2.Application.Interfaces.Media;
using MediatR;

namespace CastlePlus2.Application.Media.RodzajeMediow.Commands.UpdateRodzajMedium
{
    public sealed class UpdateRodzajMediumCommandHandler : IRequestHandler<UpdateRodzajMediumCommand, bool>
    {
        private readonly IRodzajMediumRepository _repo;

        public UpdateRodzajMediumCommandHandler(IRodzajMediumRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateRodzajMediumCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.KodRodzaju, ct);
            if (entity is null) return false;

            entity.Nazwa = request.Nazwa;

            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}