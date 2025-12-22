using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Liczniki.Commands.UpdateLicznik
{
    public class UpdateLicznikCommandHandler : IRequestHandler<UpdateLicznikCommand, LicznikDto?>
    {
        private readonly ILicznikRepository _repo;
        private readonly IMapper _mapper;

        public UpdateLicznikCommandHandler(ILicznikRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<LicznikDto?> Handle(UpdateLicznikCommand cmd, CancellationToken ct)
        {
            if (cmd.IdLicznika <= 0)
                return null;

            var req = cmd.Request ?? throw new InvalidOperationException("Brak request body.");

            req.NumerNV = (req.NumerNV ?? string.Empty).Trim();
            req.KodJednostki = (req.KodJednostki ?? string.Empty).Trim();

            if (req.IdPrzylacza <= 0)
                throw new InvalidOperationException("IdPrzylacza musi być > 0.");

            if (req.NumerNV.Length == 0)
                throw new InvalidOperationException("NumerNV jest wymagany.");

            if (req.NumerNV.Length > 60)
                throw new InvalidOperationException("NumerNV max 60 znaków.");

            if (req.KodJednostki.Length == 0)
                throw new InvalidOperationException("KodJednostki jest wymagany.");

            if (req.KodJednostki.Length > 20)
                throw new InvalidOperationException("KodJednostki max 20 znaków.");

            var entity = await _repo.GetForUpdateAsync(cmd.IdLicznika, ct);
            if (entity is null)
                return null;

            // FK: Przylacze musi istnieć
            if (!await _repo.PrzylaczeExistsAsync(req.IdPrzylacza, ct))
                throw new InvalidOperationException("Nie istnieje Przylacze dla podanego IdPrzylacza.");

            // FK: JednostkaMiary musi istnieć (KodJednostki)
            if (!await _repo.JednostkaExistsAsync(req.KodJednostki, ct))
                throw new InvalidOperationException("Nie istnieje JednostkaMiary dla podanego KodJednostki.");

            // Self-FK: licznik nadrzędny (jeśli podany)
            if (req.IdLicznikaNadrzednego.HasValue)
            {
                if (req.IdLicznikaNadrzednego.Value <= 0)
                    throw new InvalidOperationException("IdLicznikaNadrzednego musi być > 0.");

                if (req.IdLicznikaNadrzednego.Value == cmd.IdLicznika)
                    throw new InvalidOperationException("Licznik nie może być nadrzędny sam dla siebie.");

                if (!await _repo.LicznikExistsAsync(req.IdLicznikaNadrzednego.Value, ct))
                    throw new InvalidOperationException("Nie istnieje Licznik nadrzędny dla podanego IdLicznikaNadrzednego.");
            }

            // UX: NumerNV unikalny (z wykluczeniem aktualnego rekordu)
            if (await _repo.NumerExistsAsync(req.NumerNV, cmd.IdLicznika, ct))
                throw new InvalidOperationException("Istnieje już licznik o takim NumerNV (unikalny).");

            entity.IdPrzylacza = req.IdPrzylacza;
            entity.IdLicznikaNadrzednego = req.IdLicznikaNadrzednego;
            entity.NumerNV = req.NumerNV;
            entity.KodJednostki = req.KodJednostki;
            entity.WspolczynnikPrzeliczeniowy = req.WspolczynnikPrzeliczeniowy;
            entity.Aktywny = req.Aktywny;

            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<LicznikDto>(entity);
        }
    }
}
