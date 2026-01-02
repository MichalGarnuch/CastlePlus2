using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Application.Interfaces.Utrzymanie;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using CastlePlus2.Domain.Entities.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.Zgloszenia.Commands.ZglosUsterke
{
    public class ZglosUsterkeCommandHandler : IRequestHandler<ZglosUsterkeCommand, ZglosUsterkeResult>
    {
        private readonly IEncjaRepository _encjaRepository;
        private readonly IZleceniePracyRepository _zleceniePracyRepository;

        public ZglosUsterkeCommandHandler(
            IEncjaRepository encjaRepository,
            IZleceniePracyRepository zleceniePracyRepository)
        {
            _encjaRepository = encjaRepository;
            _zleceniePracyRepository = zleceniePracyRepository;
        }

        public async Task<ZglosUsterkeResult> Handle(ZglosUsterkeCommand request, CancellationToken ct)
        {
            var encja = await _encjaRepository.GetByIdAsync(request.IdEncjiGospodarza, ct);
            if (encja is null)
            {
                throw new InvalidOperationException(
                    $"Nie znaleziono encji gospodarza o Id = {request.IdEncjiGospodarza}.");
            }

            var zlecenie = new ZleceniePracy
            {
                IdEncjiGospodarza = request.IdEncjiGospodarza,
                Tytul = request.Tytul.Trim(),
                Opis = string.IsNullOrWhiteSpace(request.Opis) ? null : request.Opis.Trim(),
                Status = "NOWE",
                DataUtworzenia = DateTime.UtcNow,
                DataZamkniecia = null
            };

            var powiazanie = new PowiazanieZlecenia
            {
                IdEncji = request.IdEncjiGospodarza,
                ZleceniePracy = zlecenie
            };

            zlecenie.Powiazania.Add(powiazanie);

            await _zleceniePracyRepository.AddAsync(zlecenie, ct);
            await _zleceniePracyRepository.SaveChangesAsync(ct);

            return new ZglosUsterkeResult
            {
                IdZlecenia = zlecenie.IdZlecenia
            };
        }
    }
}