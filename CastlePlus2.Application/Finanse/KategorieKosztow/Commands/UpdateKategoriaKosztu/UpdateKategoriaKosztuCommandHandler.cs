using CastlePlus2.Application.Interfaces.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Commands.UpdateKategoriaKosztu
{
    public class UpdateKategoriaKosztuCommandHandler : IRequestHandler<UpdateKategoriaKosztuCommand, bool>
    {
        private readonly IKategoriaKosztuRepository _repo;

        public UpdateKategoriaKosztuCommandHandler(IKategoriaKosztuRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateKategoriaKosztuCommand request, CancellationToken ct)
        {
            var kod = (request.Kod ?? string.Empty).Trim();
            var nazwa = (request.Nazwa ?? string.Empty).Trim();

            var entity = await _repo.GetForUpdateAsync(request.IdKategoriiKosztu, ct);
            if (entity is null)
                return false;

            if (await _repo.ExistsOtherByKodAsync(kod, request.IdKategoriiKosztu, ct))
                throw new InvalidOperationException("Kategoria o takim Kod już istnieje (unikalny indeks).");

            entity.Kod = kod;
            entity.Nazwa = nazwa;

            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}