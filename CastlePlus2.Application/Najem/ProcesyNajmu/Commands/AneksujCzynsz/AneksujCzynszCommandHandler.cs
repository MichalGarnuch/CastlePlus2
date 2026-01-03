using CastlePlus2.Application.Common.Exceptions;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Domain.Entities.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.ProcesyNajmu.Commands.AneksujCzynsz
{
    public class AneksujCzynszCommandHandler : IRequestHandler<AneksujCzynszCommand, AneksujCzynszResult>
    {
        private readonly IUmowaNajmuRepository _umowaRepository;
        private readonly ISkladnikCzynszuRepository _skladnikRepository;

        public AneksujCzynszCommandHandler(IUmowaNajmuRepository umowaRepository, ISkladnikCzynszuRepository skladnikRepository)
        {
            _umowaRepository = umowaRepository;
            _skladnikRepository = skladnikRepository;
        }

        public async Task<AneksujCzynszResult> Handle(AneksujCzynszCommand request, CancellationToken ct)
        {
            var umowa = await _umowaRepository.GetByIdAsync(request.IdUmowyNajmu, ct);
            if (umowa is null)
                throw new InvalidOperationException("Nie znaleziono umowy najmu o podanym IdUmowyNajmu.");

            var dataPoczatku = DateOnly.FromDateTime(umowa.DataPoczatku);
            var dataZakonczenia = umowa.DataZakonczenia is null
                ? (DateOnly?)null
                : DateOnly.FromDateTime(umowa.DataZakonczenia.Value);

            if (request.OdDnia < dataPoczatku || (dataZakonczenia.HasValue && request.OdDnia > dataZakonczenia.Value))
                throw new BusinessConflictException("Data rozpoczęcia aneksu jest poza zakresem obowiązywania umowy.");

            var active = await _skladnikRepository.GetActiveByNameAsync(request.IdUmowyNajmu, request.Nazwa, request.OdDnia, ct);
            if (active is not null && request.OdDnia <= active.OdDnia)
                throw new BusinessConflictException("Data rozpoczęcia aneksu musi być późniejsza niż obowiązujący składnik.");

            var excludeId = active?.IdSkladnikaCzynszu;
            var overlapExists = await _skladnikRepository.ExistsOverlapAsync(
                request.IdUmowyNajmu,
                request.Nazwa,
                request.OdDnia,
                excludeId,
                ct);

            if (overlapExists)
                throw new BusinessConflictException("Istnieje już składnik czynszu dla tej umowy w podanym okresie.");

            if (active is not null)
            {
                active.DoDnia = request.OdDnia.AddDays(-1);
            }

            var now = new SkladnikCzynszu
            {
                IdUmowyNajmu = request.IdUmowyNajmu,
                Nazwa = request.Nazwa,
                KodJednostki = request.KodJednostki,
                Stawka = request.Stawka,
                IloscBazowa = request.IloscBazowa,
                KodIndeksacji = request.KodIndeksacji,
                OdDnia = request.OdDnia,
                DoDnia = null
            };

            await _skladnikRepository.AddAsync(now, ct);
            await _skladnikRepository.SaveChangesAsync(ct);

            return new AneksujCzynszResult
            {
                IdUmowyNajmu = request.IdUmowyNajmu,
                IdSkladnikaCzynszu = now.IdSkladnikaCzynszu,
                Message = "Aneks czynszu został zapisany."
            };
        }
    }
}