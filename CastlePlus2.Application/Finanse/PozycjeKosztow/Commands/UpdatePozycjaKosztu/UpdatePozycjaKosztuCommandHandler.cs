using CastlePlus2.Application.Interfaces.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.PozycjeKosztow.Commands.UpdatePozycjaKosztu
{
    public class UpdatePozycjaKosztuCommandHandler : IRequestHandler<UpdatePozycjaKosztuCommand, bool>
    {
        private readonly IPozycjaKosztuRepository _repo;

        public UpdatePozycjaKosztuCommandHandler(IPozycjaKosztuRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdatePozycjaKosztuCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdPozycjiKosztu, ct);
            if (entity is null)
                return false;

            entity.IdFaktury = request.IdFaktury;
            entity.IdKategoriiKosztu = request.IdKategoriiKosztu;
            entity.Opis = string.IsNullOrWhiteSpace(request.Opis) ? null : request.Opis.Trim();
            entity.KwotaNetto = request.KwotaNetto;
            entity.KwotaBrutto = request.KwotaBrutto;

            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}