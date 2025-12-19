using CastlePlus2.Application.Interfaces.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.Indeksacje.Commands.UpdateIndeksacja
{
    public class UpdateIndeksacjaCommandHandler : IRequestHandler<UpdateIndeksacjaCommand, bool>
    {
        private readonly IIndeksacjaRepository _repo;

        public UpdateIndeksacjaCommandHandler(IIndeksacjaRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateIndeksacjaCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByKodAsync(request.KodIndeksacji, ct);
            if (entity is null) return false;

            entity.Nazwa = request.Nazwa;
            entity.AdresZrodlaURL = request.AdresZrodlaURL;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
