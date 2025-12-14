using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using CastlePlus2.Domain.Entities.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Liczniki.Commands.CreateLicznik
{
    public class CreateLicznikCommandHandler : IRequestHandler<CreateLicznikCommand, LicznikDto>
    {
        private readonly ILicznikRepository _repo;
        private readonly IMapper _mapper;

        public CreateLicznikCommandHandler(ILicznikRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<LicznikDto> Handle(CreateLicznikCommand request, CancellationToken ct)
        {
            request.NumerNV = (request.NumerNV ?? string.Empty).Trim();
            request.KodJednostki = (request.KodJednostki ?? string.Empty).Trim();

            if (request.IdPrzylacza <= 0)
                throw new InvalidOperationException("IdPrzylacza musi być > 0.");

            if (request.NumerNV.Length == 0)
                throw new InvalidOperationException("NumerNV jest wymagany.");

            if (request.NumerNV.Length > 60)
                throw new InvalidOperationException("NumerNV max 60 znaków.");

            if (request.KodJednostki.Length == 0)
                throw new InvalidOperationException("KodJednostki jest wymagany.");

            if (request.KodJednostki.Length > 20)
                throw new InvalidOperationException("KodJednostki max 20 znaków.");

            // FK: Przylacze musi istnieć
            if (!await _repo.PrzylaczeExistsAsync(request.IdPrzylacza, ct))
                throw new InvalidOperationException("Nie istnieje Przylacze dla podanego IdPrzylacza.");

            // FK: Jednostka miary musi istnieć
            if (!await _repo.JednostkaExistsAsync(request.KodJednostki, ct))
                throw new InvalidOperationException("Nie istnieje JednostkaMiary dla podanego KodJednostki.");

            // Self-FK: licznik nadrzędny (jeśli podany)
            if (request.IdLicznikaNadrzednego.HasValue)
            {
                if (request.IdLicznikaNadrzednego.Value <= 0)
                    throw new InvalidOperationException("IdLicznikaNadrzednego musi być > 0.");

                if (!await _repo.LicznikExistsAsync(request.IdLicznikaNadrzednego.Value, ct))
                    throw new InvalidOperationException("Nie istnieje Licznik nadrzędny dla podanego IdLicznikaNadrzednego.");
            }

            // UX: NumerNV unikalny
            if (await _repo.NumerExistsAsync(request.NumerNV, ct))
                throw new InvalidOperationException("Istnieje już licznik o takim NumerNV (unikalny).");

            var entity = new Licznik
            {
                IdPrzylacza = request.IdPrzylacza,
                IdLicznikaNadrzednego = request.IdLicznikaNadrzednego,
                NumerNV = request.NumerNV,
                KodJednostki = request.KodJednostki,
                WspolczynnikPrzeliczeniowy = request.WspolczynnikPrzeliczeniowy,
                Aktywny = request.Aktywny
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<LicznikDto>(entity);
        }
    }
}
