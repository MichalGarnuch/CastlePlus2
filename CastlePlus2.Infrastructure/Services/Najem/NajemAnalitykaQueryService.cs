using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Services.Najem
{
    public sealed class NajemAnalitykaQueryService : INajemAnalitykaQueryService
    {
        private readonly CastlePlus2DbContext _db;

        public NajemAnalitykaQueryService(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<OblozenieLokaluDto>> GetOblozenieLokaliUtcDzisAsync(CancellationToken ct)
        {
            var rows = await _db.Database
                .SqlQueryRaw<OblozenieLokaluDto>(@"
SELECT
    IdNieruchomosci,
    NazwaNieruchomosci,
    IdBudynku,
    KodBudynku,
    IdLokalu,
    KodLokalu,
    CzyZajety,
    IdUmowyNajmu,
    NajemcaNazwa,
    UmowaOd,
    UmowaDo
FROM [rdzen].[vw_OblozenieLokali_UtcDzis]")
                .ToListAsync(ct);

            return rows;
        }

        public async Task<IReadOnlyList<RaportNajmuZaMiesiacRowDto>> GetRaportNajmuZaMiesiacAsync(int rok, int miesiac, CancellationToken ct)
        {
            var rokParameter = new SqlParameter("@Rok", rok);
            var miesiacParameter = new SqlParameter("@Miesiac", miesiac);

            var rows = await _db.Database
                .SqlQueryRaw<RaportNajmuZaMiesiacRowDto>(
                    "EXEC [najem].[usp_RaportNajmuZaMiesiac] @Rok, @Miesiac",
                    rokParameter,
                    miesiacParameter)
                .ToListAsync(ct);

            return rows;
        }
    }
}