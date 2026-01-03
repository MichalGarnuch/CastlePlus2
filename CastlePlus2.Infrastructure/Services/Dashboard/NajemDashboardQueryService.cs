using CastlePlus2.Application.Interfaces.Dashboard;
using CastlePlus2.Contracts.DTOs.Dashboard;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Services.Dashboard
{
    public class NajemDashboardQueryService : INajemDashboardQueryService
    {
        private readonly CastlePlus2DbContext _db;

        public NajemDashboardQueryService(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<NajemDashboardDto> GetNajemDashboardAsync(
            DateOnly today,
            DateOnly koniecOkresu,
            CancellationToken ct)
        {
            var lokaleRazem = await _db.Lokale
                .AsNoTracking()
                .Select(x => x.Id)
                .Distinct()
                .CountAsync(ct);

            var lokaleZajete = await _db.PrzedmiotyNajmu
                .AsNoTracking()
                .Where(x => x.OdDnia <= today && (x.DoDnia == null || x.DoDnia >= today))
                .Select(x => x.IdEncji)
                .Distinct()
                .CountAsync(ct);

            var endDateTime = koniecOkresu.ToDateTime(TimeOnly.MaxValue);
            var startDateTime = today.ToDateTime(TimeOnly.MinValue);

            // 1) Baza umów wygasających
            var umowy = await _db.UmowyNajmu
                .AsNoTracking()
                .Where(x => x.DataZakonczenia != null
                            && x.DataZakonczenia >= startDateTime
                            && x.DataZakonczenia <= endDateTime)
                .OrderBy(x => x.DataZakonczenia)
                .Select(x => new
                {
                    x.Id,
                    x.DataZakonczenia,
                    x.IdNajemcy,
                    x.IdWynajmujacego,
                    KodUmowy = x.KodEncji // <- jeśli u Ciebie inaczej: podmień (np. x.KodUmowy)
                })
                .Take(10)
                .ToListAsync(ct);

            var umowaIds = umowy.Select(x => x.Id).ToList();
            var lokalIds = new List<Guid>();

            // 2) Przedmioty najmu dla tych umów (żeby wziąć lokale)
            var przedmioty = await _db.PrzedmiotyNajmu
                .AsNoTracking()
                .Where(p => umowaIds.Contains(p.IdUmowyNajmu))
                .Select(p => new
                {
                    p.IdUmowyNajmu,
                    p.IdEncji
                })
                .ToListAsync(ct);

            lokalIds = przedmioty.Select(x => x.IdEncji).Distinct().ToList();

            // 3) Lokale + budynki (kod lokalu + kod budynku)
            var lokale = await _db.Lokale
                .AsNoTracking()
                .Where(l => lokalIds.Contains(l.Id))
                .Select(l => new
                {
                    l.Id,
                    l.KodLokalu,
                    l.IdBudynku
                })
                .ToListAsync(ct);

            var budynekIds = lokale.Select(x => x.IdBudynku).Distinct().ToList();

            var budynki = await _db.Budynki
                .AsNoTracking()
                .Where(b => budynekIds.Contains(b.Id))
                .Select(b => new
                {
                    b.Id,
                    b.KodBudynku
                })
                .ToListAsync(ct);

            var budynekById = budynki.ToDictionary(x => x.Id, x => x.KodBudynku);
            var lokalById = lokale.ToDictionary(x => x.Id, x => new
            {
                x.KodLokalu,
                x.IdBudynku
            });

            // 4) Nazwy podmiotów (najemca i wynajmujący)
            var podmiotIds = umowy
                .SelectMany(x => new[] { x.IdNajemcy, x.IdWynajmujacego })
                .Distinct()
                .ToList();

            var podmioty = await _db.Podmioty
                .AsNoTracking()
                .Where(p => podmiotIds.Contains(p.IdPodmiotu))
                .Select(p => new
                {
                    p.IdPodmiotu,
                    p.Nazwa
                })
                .ToListAsync(ct);

            var podmiotById = podmioty.ToDictionary(x => x.IdPodmiotu, x => x.Nazwa);

            // 5) Składanie wierszy dashboardu
            var wygasajaceUmowy = umowy
                .Where(x => x.DataZakonczenia.HasValue)
                .Select(x =>
                {
                    // Lista lokali dla danej umowy
                    var lokForUmowa = przedmioty
                        .Where(p => p.IdUmowyNajmu == x.Id)
                        .Select(p => p.IdEncji)
                        .Distinct()
                        .Where(id => lokalById.ContainsKey(id)) // filtrujemy zanim zrobimy tuple
                        .Select(id =>
                        {
                        var loc = lokalById[id];
                        var budCode = budynekById.TryGetValue(loc.IdBudynku, out var bc) ? bc : "BUD-?";
                            return (budCode, KodLokalu: loc.KodLokalu);
                        })
                        .ToList();


                    // Grupowanie: "BUD-001: LOK-1, LOK-2 | BUD-002: LOK-9"
                    var przedmiotText = string.Empty;
                    if (lokForUmowa.Count > 0)
                    {
                        przedmiotText = string.Join(" | ",
                            lokForUmowa
                                .GroupBy(t => t.budCode)
                                .Select(g => $"{g.Key}: {string.Join(", ", g.Select(x2 => x2.KodLokalu))}"));
                    }

                    var najemcaName = podmiotById.TryGetValue(x.IdNajemcy, out var nn) ? nn : $"Podmiot {x.IdNajemcy}";
                    var wynName = podmiotById.TryGetValue(x.IdWynajmujacego, out var wn) ? wn : $"Podmiot {x.IdWynajmujacego}";

                    return new WygasajacaUmowaDto
                    {
                        IdUmowy = x.Id,
                        KodUmowy = x.KodUmowy,
                        DataZakonczenia = DateOnly.FromDateTime(x.DataZakonczenia!.Value),
                        Najemca = najemcaName,
                        Wynajmujacy = wynName,
                        PrzedmiotNajmu = przedmiotText
                    };
                })
                .ToList();

            return new NajemDashboardDto
            {
                LokaleRazem = lokaleRazem,
                LokaleZajete = lokaleZajete,
                LokaleWolne = Math.Max(0, lokaleRazem - lokaleZajete),
                WygasajaceUmowy = wygasajaceUmowy
            };
        }
    }
}
