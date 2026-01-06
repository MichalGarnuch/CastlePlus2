using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Domain.Entities.Podmioty;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Commands.UstawWlasnosc
{
    public class UstawWlasnoscCommandHandler : IRequestHandler<UstawWlasnoscCommand, IReadOnlyList<WlasnoscDto>>
    {
        private const decimal DocelowaSumaUdzialow = 100m;
        private const decimal TolerancjaSumaUdzialow = 0.01m;

        private readonly IWlasnoscRepository _wlasnoscRepository;
        private readonly IMapper _mapper;

        public UstawWlasnoscCommandHandler(IWlasnoscRepository wlasnoscRepository, IMapper mapper)
        {
            _wlasnoscRepository = wlasnoscRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<WlasnoscDto>> Handle(UstawWlasnoscCommand request, CancellationToken ct)
        {
            if (request.IdEncji == Guid.Empty)
                throw new InvalidOperationException("IdEncji jest wymagane.");

            if (request.Udzialy.Count == 0)
                throw new InvalidOperationException("Lista udziałów nie może być pusta.");

            if (!await _wlasnoscRepository.EncjaExistsAsync(request.IdEncji, ct))
                throw new InvalidOperationException("Nie istnieje Encja o podanym IdEncji.");

            var sumaUdzialow = request.Udzialy.Sum(x => x.UdzialProcent);
            if (Math.Abs(sumaUdzialow - DocelowaSumaUdzialow) > TolerancjaSumaUdzialow)
                throw new InvalidOperationException("Suma udziałów musi wynosić 100.00 (tolerancja 0.01).");

            var podmiotySprawdzone = new HashSet<long>();

            foreach (var udzial in request.Udzialy)
            {
                if (udzial.IdPodmiotu <= 0)
                    throw new InvalidOperationException("IdPodmiotu musi być > 0.");

                if (udzial.UdzialProcent <= 0 || udzial.UdzialProcent > 100)
                    throw new InvalidOperationException("UdzialProcent musi być w zakresie (0, 100].");

                if (udzial.DoDnia.HasValue && udzial.DoDnia.Value < udzial.OdDnia)
                    throw new InvalidOperationException("DoDnia nie może być wcześniejsze niż OdDnia.");

                if (!podmiotySprawdzone.Contains(udzial.IdPodmiotu))
                {
                    if (!await _wlasnoscRepository.PodmiotExistsAsync(udzial.IdPodmiotu, ct))
                        throw new InvalidOperationException($"Nie istnieje Podmiot o IdPodmiotu={udzial.IdPodmiotu}.");

                    podmiotySprawdzone.Add(udzial.IdPodmiotu);
                }
            }

            // POPRAWKA: nie blokujemy równoległych udziałów różnych podmiotów (np. 60/40 w tym samym czasie).
            // Blokujemy tylko nakładanie okresów dla TEGO SAMEGO podmiotu.
            if (MaNakladajaceSieOkresyDlaTegoSamegoPodmiotu(request.Udzialy))
                throw new InvalidOperationException("Okresy udziałów nie mogą się nakładać dla tego samego podmiotu.");

            var obecne = await _wlasnoscRepository.GetForUpdateByEncjaIdAsync(request.IdEncji, ct);
            foreach (var wpis in obecne)
            {
                _wlasnoscRepository.Remove(wpis);
            }

            var nowe = new List<Wlasnosc>();

            foreach (var udzial in request.Udzialy)
            {
                var entity = new Wlasnosc
                {
                    IdEncji = request.IdEncji,
                    IdPodmiotu = udzial.IdPodmiotu,
                    UdzialProcent = udzial.UdzialProcent,
                    OdDnia = udzial.OdDnia,
                    DoDnia = udzial.DoDnia
                };

                await _wlasnoscRepository.AddAsync(entity, ct);
                nowe.Add(entity);
            }

            await _wlasnoscRepository.SaveChangesAsync(ct);

            return nowe.Select(x => _mapper.Map<WlasnoscDto>(x)).ToList();
        }

        private static bool MaNakladajaceSieOkresyDlaTegoSamegoPodmiotu(IReadOnlyList<UstawWlasnoscUdzialCommand> udzialy)
        {
            foreach (var grupa in udzialy.GroupBy(x => x.IdPodmiotu))
            {
                var list = grupa
                    .OrderBy(x => x.OdDnia)
                    .ToList();

                for (var i = 0; i < list.Count; i++)
                {
                    var a = list[i];
                    var aEnd = a.DoDnia ?? DateOnly.MaxValue;

                    for (var j = i + 1; j < list.Count; j++)
                    {
                        var b = list[j];
                        var bEnd = b.DoDnia ?? DateOnly.MaxValue;

                        if (a.OdDnia <= bEnd && b.OdDnia <= aEnd)
                            return true;
                    }
                }
            }

            return false;
        }
    }
}
