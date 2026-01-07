using CastlePlus2.Application.Interfaces.Dashboard;
using CastlePlus2.Contracts.DTOs.Dashboard;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Services.Dashboard
{
    public class DashboardV1NajemQueryService : IDashboardV1NajemQueryService
    {
        private readonly CastlePlus2DbContext _db;

        public DashboardV1NajemQueryService(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<DashboardV1NajemDto> GetDashboardV1NajemAsync(
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

            var startDateTime = today.ToDateTime(TimeOnly.MinValue);
            var endDateTime = koniecOkresu.ToDateTime(TimeOnly.MaxValue);

            var umowy = await _db.UmowyNajmu
                .AsNoTracking()
                .Where(x => x.DataZakonczenia != null
                            && x.DataZakonczenia >= startDateTime
                            && x.DataZakonczenia <= endDateTime)
                .OrderBy(x => x.DataZakonczenia)
                .Take(10)
                .Select(x => new
                {
                    x.Id,
                    x.DataZakonczenia,
                    x.IdNajemcy,
                    x.IdWynajmujacego
                })
                .ToListAsync(ct);

            var umowaIds = umowy.Select(x => x.Id).ToList();

            var przedmioty = new List<(Guid IdUmowyNajmu, Guid IdEncji)>();
            if (umowaIds.Count > 0)
            {
                var przedmiotyRows = await _db.PrzedmiotyNajmu
                    .AsNoTracking()
                    .Where(p => umowaIds.Contains(p.IdUmowyNajmu))
                    .Select(p => new { p.IdUmowyNajmu, p.IdEncji })
                    .ToListAsync(ct);

                przedmioty = przedmiotyRows.Select(x => (x.IdUmowyNajmu, x.IdEncji)).ToList();
            }

            var lokalIds = przedmioty.Select(x => x.IdEncji).Distinct().ToList();

            var lokale = new List<(Guid Id, string KodLokalu)>();
            if (lokalIds.Count > 0)
            {
                var lokaleRows = await _db.Lokale
                    .AsNoTracking()
                    .Where(l => lokalIds.Contains(l.Id))
                    .Select(l => new { l.Id, l.KodLokalu })
                    .ToListAsync(ct);

                lokale = lokaleRows.Select(x => (x.Id, x.KodLokalu)).ToList();
            }

            var lokalById = lokale.ToDictionary(x => x.Id, x => x.KodLokalu);

            var umowyDto = umowy.Select(x =>
            {
                var lokaleUmowy = przedmioty
                    .Where(p => p.IdUmowyNajmu == x.Id)
                    .Select(p => p.IdEncji)
                    .Distinct()
                    .Where(id => lokalById.ContainsKey(id))
                    .Select(id => lokalById[id])
                    .OrderBy(kod => kod)
                    .ToList();

                var przedmiotText = lokaleUmowy.Count == 0
                    ? "-"
                    : string.Join(", ", lokaleUmowy);

                return new DashboardV1NajemUmowaDto
                {
                    IdUmowy = x.Id,
                    DataZakonczenia = x.DataZakonczenia.HasValue
                        ? DateOnly.FromDateTime(x.DataZakonczenia.Value)
                        : null,
                    IdNajemcy = x.IdNajemcy,
                    IdWynajmujacego = x.IdWynajmujacego,
                    PrzedmiotNajmu = przedmiotText
                };
            }).ToList();

            var zalegleFaktury = await _db.Faktury
                .AsNoTracking()
                .GroupJoin(
                    _db.RozliczeniaPlatnosci.AsNoTracking(),
                    f => f.IdFaktury,
                    r => r.IdFaktury,
                    (f, rozliczenia) => new
                    {
                        f.IdFaktury,
                        f.NumerFaktury,
                        f.DataWystawienia,
                        f.KodWaluty,
                        KwotaBrutto = f.KwotaBrutto ?? 0m,
                        KwotaRozliczona = rozliczenia.Sum(r => (decimal?)r.Kwota) ?? 0m
                    })
                .Select(x => new
                {
                    x.IdFaktury,
                    x.NumerFaktury,
                    x.DataWystawienia,
                    x.KodWaluty,
                    x.KwotaBrutto,
                    KwotaPozostala = x.KwotaBrutto - x.KwotaRozliczona
                })
                .Where(x => x.KwotaPozostala > 0m)
                .OrderByDescending(x => x.KwotaPozostala)
                .ThenBy(x => x.DataWystawienia)
                .Take(10)
                .ToListAsync(ct);

            var fakturyDto = zalegleFaktury.Select(x => new DashboardV1ZaleglaFakturaDto
            {
                IdFaktury = x.IdFaktury,
                NumerFaktury = x.NumerFaktury,
                DataWystawienia = DateOnly.FromDateTime(x.DataWystawienia),
                KodWaluty = x.KodWaluty,
                KwotaBrutto = x.KwotaBrutto,
                KwotaPozostala = x.KwotaPozostala
            }).ToList();

            return new DashboardV1NajemDto
            {
                LokaleRazem = lokaleRazem,
                LokaleZajete = lokaleZajete,
                LokaleWolne = Math.Max(0, lokaleRazem - lokaleZajete),
                UmowyKonczaSieWkrotce = umowyDto,
                ZalegleFaktury = fakturyDto
            };
        }
    }
}