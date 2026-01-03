using CastlePlus2.Application.Common.Exceptions;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Domain.Entities.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.ProcesyNajmu.Commands.ZakonczUmoweNajmu
{
    public class ZakonczUmoweNajmuCommandHandler : IRequestHandler<ZakonczUmoweNajmuCommand, ZakonczUmoweNajmuResult>
    {
        private readonly IUmowaNajmuRepository _umowaRepository;
        private readonly IPrzedmiotNajmuRepository _przedmiotRepository;
        private readonly ISkladnikCzynszuRepository _skladnikRepository;
        private readonly IKaucjaRepository _kaucjaRepository;

        public ZakonczUmoweNajmuCommandHandler(
            IUmowaNajmuRepository umowaRepository,
            IPrzedmiotNajmuRepository przedmiotRepository,
            ISkladnikCzynszuRepository skladnikRepository,
            IKaucjaRepository kaucjaRepository)
        {
            _umowaRepository = umowaRepository;
            _przedmiotRepository = przedmiotRepository;
            _skladnikRepository = skladnikRepository;
            _kaucjaRepository = kaucjaRepository;
        }

        public async Task<ZakonczUmoweNajmuResult> Handle(ZakonczUmoweNajmuCommand request, CancellationToken ct)
        {
            var umowa = await _umowaRepository.GetForUpdateAsync(request.IdUmowyNajmu, ct);
            if (umowa is null)
                throw new InvalidOperationException("Nie znaleziono umowy najmu o podanym IdUmowyNajmu.");

            if (umowa.DataZakonczenia.HasValue)
                throw new BusinessConflictException("Umowa najmu jest już zakończona.");

            var dataPoczatku = DateOnly.FromDateTime(umowa.DataPoczatku);
            if (request.DataZakonczenia < dataPoczatku)
                throw new BusinessConflictException("Data zakończenia nie może być wcześniejsza niż data rozpoczęcia umowy.");

            umowa.DataZakonczenia = request.DataZakonczenia.ToDateTime(TimeOnly.MinValue);

            var przedmioty = await _przedmiotRepository.GetOpenForUpdateByUmowaIdAsync(request.IdUmowyNajmu, request.DataZakonczenia, ct);
            foreach (var przedmiot in przedmioty)
            {
                przedmiot.DoDnia = request.DataZakonczenia;
            }

            var skladniki = await _skladnikRepository.GetOpenForUpdateByUmowaIdAsync(request.IdUmowyNajmu, request.DataZakonczenia, ct);
            foreach (var skladnik in skladniki)
            {
                skladnik.DoDnia = request.DataZakonczenia;
            }

            Kaucja? zwrot = null;
            if (request.KwotaZwrotuKaucji.HasValue)
            {
                zwrot = new Kaucja
                {
                    IdUmowyNajmu = request.IdUmowyNajmu,
                    RodzajOperacji = "ZWROT",
                    Kwota = request.KwotaZwrotuKaucji.Value,
                    KodWaluty = umowa.KodWaluty,
                    DataOperacji = request.DataZakonczenia
                };

                await _kaucjaRepository.AddAsync(zwrot, ct);
            }

            await _umowaRepository.SaveChangesAsync(ct);

            return new ZakonczUmoweNajmuResult
            {
                IdUmowyNajmu = request.IdUmowyNajmu,
                DataZakonczenia = request.DataZakonczenia,
                IdOperacjiKaucji = zwrot?.IdOperacjiKaucji,
                Message = "Umowa najmu została zakończona."
            };
        }
    }
}