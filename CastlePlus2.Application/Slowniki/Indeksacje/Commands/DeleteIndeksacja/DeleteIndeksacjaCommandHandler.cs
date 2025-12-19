using CastlePlus2.Application.Interfaces.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.Indeksacje.Commands.DeleteIndeksacja
{
    public class DeleteIndeksacjaCommandHandler : IRequestHandler<DeleteIndeksacjaCommand, bool>
    {
        private readonly IIndeksacjaRepository _repo;

        public DeleteIndeksacjaCommandHandler(IIndeksacjaRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteIndeksacjaCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByKodAsync(request.KodIndeksacji, ct);
            if (entity is null) return false;

            await _repo.RemoveAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}
