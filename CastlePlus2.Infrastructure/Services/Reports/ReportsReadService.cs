using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Reports;
using CastlePlus2.Contracts.Reports;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Services.Reports;

public sealed class ReportsReadService : IReportsReadService
{
    private readonly CastlePlus2DbContext _dbContext;

    public ReportsReadService(CastlePlus2DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PodsumowanieOperacyjneRow> GetPodsumowanieAsync(CancellationToken ct)
    {
        var liczbaNieruchomosci = await _dbContext.Nieruchomosci.CountAsync(ct);
        var liczbaBudynkow = await _dbContext.Budynki.CountAsync(ct);
        var liczbaLokali = await _dbContext.Lokale.CountAsync(ct);
        var liczbaPodmiotow = await _dbContext.Podmioty.CountAsync(ct);
        var liczbaUmowNajmu = await _dbContext.UmowyNajmu.CountAsync(ct);

        return new PodsumowanieOperacyjneRow(
            GeneratedAt: DateTime.UtcNow,
            LiczbaNieruchomosci: liczbaNieruchomosci,
            LiczbaBudynkow: liczbaBudynkow,
            LiczbaLokali: liczbaLokali,
            LiczbaPodmiotow: liczbaPodmiotow,
            LiczbaUmowNajmu: liczbaUmowNajmu);
    }

    public async Task<IReadOnlyList<FakturyStatRow>> GetFakturyAsync(int take, CancellationToken ct)
    {
        return await _dbContext.Faktury
            .AsNoTracking()
            .Include(faktura => faktura.Podmiot)
            .OrderByDescending(faktura => faktura.DataWystawienia)
            .Take(take)
            .Select(faktura => new FakturyStatRow(
                faktura.NumerFaktury,
                faktura.DataWystawienia,
                faktura.Podmiot != null ? faktura.Podmiot.Nazwa : null,
                faktura.KodWaluty,
                faktura.KwotaNetto,
                faktura.KwotaBrutto))
            .ToListAsync(ct);
    }
}