using CastlePlus2.Application.Interfaces.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.AlokacjeKosztow.Commands.UpdateAlokacjaKosztu
{
    public class UpdateAlokacjaKosztuCommandHandler : IRequestHandler<UpdateAlokacjaKosztuCommand, bool>
    {
        private readonly IAlokacjaKosztuRepository _repo;

        public UpdateAlokacjaKosztuCommandHandler(IAlokacjaKosztuRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateAlokacjaKosztuCommand request, CancellationToken ct)
        {
            var existing = await _repo.GetForUpdateAsync(request.IdAlokacji, ct);
            if (existing is null) return false;

            if (!await _repo.EncjaExistsAsync(request.IdEncji, ct))
                throw new InvalidOperationException($"Encja nie istnieje: IdEncji={request.IdEncji}");

            if (!await _repo.PozycjaKosztuExistsAsync(request.IdPozycjiKosztu, ct))
                throw new InvalidOperationException($"PozycjaKosztu nie istnieje: IdPozycjiKosztu={request.IdPozycjiKosztu}");

            if (await _repo.ExistsOtherAsync(request.IdPozycjiKosztu, request.IdEncji, request.IdAlokacji, ct))
                throw new InvalidOperationException("Taka alokacja (IdPozycjiKosztu + IdEncji) już istnieje dla innego rekordu.");

            existing.IdPozycjiKosztu = request.IdPozycjiKosztu;
            existing.IdEncji = request.IdEncji;
            existing.KwotaNetto = request.KwotaNetto;
            existing.KwotaBrutto = request.KwotaBrutto;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
