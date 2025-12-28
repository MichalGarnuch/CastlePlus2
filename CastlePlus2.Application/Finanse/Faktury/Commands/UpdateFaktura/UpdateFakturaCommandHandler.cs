using CastlePlus2.Application.Interfaces.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Faktury.Commands.UpdateFaktura
{
    public class UpdateFakturaCommandHandler : IRequestHandler<UpdateFakturaCommand, bool>
    {
        private readonly IFakturaRepository _repo;

        public UpdateFakturaCommandHandler(IFakturaRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateFakturaCommand cmd, CancellationToken ct)
        {
            cmd.NumerFaktury = (cmd.NumerFaktury ?? string.Empty).Trim();
            cmd.KodWaluty = (cmd.KodWaluty ?? string.Empty).Trim().ToUpperInvariant();

            if (cmd.NumerFaktury.Length == 0)
                throw new InvalidOperationException("NumerFaktury jest wymagany.");

            if (cmd.NumerFaktury.Length > 60)
                throw new InvalidOperationException("NumerFaktury max 60 znaków.");

            if (cmd.IdPodmiotu <= 0)
                throw new InvalidOperationException("IdPodmiotu musi być > 0.");

            if (cmd.DataWystawienia == default)
                throw new InvalidOperationException("DataWystawienia jest wymagana.");

            if (cmd.KodWaluty.Length != 3)
                throw new InvalidOperationException("KodWaluty musi mieć dokładnie 3 znaki.");

            // UNIQUE: NumerFaktury
            if (await _repo.ExistsByNumerAsync(cmd.NumerFaktury, cmd.IdFaktury, ct))
                throw new InvalidOperationException("Istnieje już faktura o podanym NumerFaktury.");

            var entity = await _repo.GetForUpdateAsync(cmd.IdFaktury, ct);
            if (entity is null)
                return false;

            entity.NumerFaktury = cmd.NumerFaktury;
            entity.IdPodmiotu = cmd.IdPodmiotu;
            entity.DataWystawienia = cmd.DataWystawienia.Date;
            entity.DataSprzedazy = cmd.DataSprzedazy?.Date;
            entity.KodWaluty = cmd.KodWaluty;
            entity.KwotaNetto = cmd.KwotaNetto;
            entity.KwotaBrutto = cmd.KwotaBrutto;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}