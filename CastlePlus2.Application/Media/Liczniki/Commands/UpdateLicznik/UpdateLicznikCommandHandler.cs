using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Liczniki.Commands.UpdateLicznik
{
    public class UpdateLicznikCommandHandler : IRequestHandler<UpdateLicznikCommand, bool>
    {
        private readonly ILicznikRepository _repo;

        public UpdateLicznikCommandHandler(ILicznikRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateLicznikCommand cmd, CancellationToken ct)
        {
            cmd.NumerNV = (cmd.NumerNV ?? string.Empty).Trim();
            cmd.KodJednostki = (cmd.KodJednostki ?? string.Empty).Trim();

            if (cmd.IdPrzylacza <= 0)
                throw new InvalidOperationException("IdPrzylacza musi być > 0.");

            if (cmd.NumerNV.Length == 0)
                throw new InvalidOperationException("NumerNV jest wymagany.");

            if (cmd.NumerNV.Length > 60)
                throw new InvalidOperationException("NumerNV max 60 znaków.");

            if (cmd.KodJednostki.Length == 0)
                throw new InvalidOperationException("KodJednostki jest wymagany.");

            if (cmd.KodJednostki.Length > 20)
                throw new InvalidOperationException("KodJednostki max 20 znaków.");

            var entity = await _repo.GetForUpdateAsync(cmd.IdLicznika, ct);
            if (entity is null)
                return false;

            // FK: Przylacze musi istnieć
            if (!await _repo.PrzylaczeExistsAsync(cmd.IdPrzylacza, ct))
                throw new InvalidOperationException("Nie istnieje Przylacze dla podanego IdPrzylacza.");

            // FK: JednostkaMiary musi istnieć (KodJednostki)
            if (!await _repo.JednostkaExistsAsync(cmd.KodJednostki, ct))
                throw new InvalidOperationException("Nie istnieje JednostkaMiary dla podanego KodJednostki.");

            // Self-FK: licznik nadrzędny (jeśli podany)
            if (cmd.IdLicznikaNadrzednego.HasValue)
            {
                if (cmd.IdLicznikaNadrzednego.Value <= 0)
                    throw new InvalidOperationException("IdLicznikaNadrzednego musi być > 0.");

                if (cmd.IdLicznikaNadrzednego.Value == cmd.IdLicznika)
                    throw new InvalidOperationException("Licznik nie może być nadrzędny sam dla siebie.");

                if (!await _repo.LicznikExistsAsync(cmd.IdLicznikaNadrzednego.Value, ct))
                    throw new InvalidOperationException("Nie istnieje Licznik nadrzędny dla podanego IdLicznikaNadrzednego.");
            }

            // UX: NumerNV unikalny (z wykluczeniem aktualnego rekordu)
            if (await _repo.NumerExistsAsync(cmd.NumerNV, cmd.IdLicznika, ct))
                throw new InvalidOperationException("Istnieje już licznik o takim NumerNV (unikalny).");

            entity.IdPrzylacza = cmd.IdPrzylacza;
            entity.IdLicznikaNadrzednego = cmd.IdLicznikaNadrzednego;
            entity.NumerNV = cmd.NumerNV;
            entity.KodJednostki = cmd.KodJednostki;
            entity.WspolczynnikPrzeliczeniowy = cmd.WspolczynnikPrzeliczeniowy;
            entity.Aktywny = cmd.Aktywny;

            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}