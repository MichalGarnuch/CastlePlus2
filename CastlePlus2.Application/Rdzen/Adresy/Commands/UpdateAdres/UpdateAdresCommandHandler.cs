using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Adresy.Commands.UpdateAdres
{
    public class UpdateAdresCommandHandler : IRequestHandler<UpdateAdresCommand, bool>
    {
        private readonly IAdresRepository _repo;

        public UpdateAdresCommandHandler(IAdresRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateAdresCommand request, CancellationToken ct)
        {
            // Pobierz do update (może być bez tracking; wtedy tworzymy nową encję i Update)
            var existing = await _repo.GetByIdAsync(request.IdAdresu, ct);
            if (existing is null) return false;

            var entity = new Adres
            {
                IdAdresu = request.IdAdresu,
                Kraj = string.IsNullOrWhiteSpace(request.Kraj) ? "Polska" : request.Kraj,
                Wojewodztwo = request.Wojewodztwo,
                Powiat = request.Powiat,
                Gmina = request.Gmina,
                Miejscowosc = request.Miejscowosc,
                Ulica = request.Ulica,
                Numer = request.Numer,
                KodPocztowy = request.KodPocztowy,
                AdresPelny = request.AdresPelny
            };

            await _repo.UpdateAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
