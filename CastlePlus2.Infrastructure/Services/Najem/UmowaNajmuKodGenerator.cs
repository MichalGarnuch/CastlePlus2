using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Services.Najem
{
    public class UmowaNajmuKodGenerator : IUmowaNajmuKodGenerator
    {
        private const string TypEncjiUmowy = "UMOWA_NAJMU";
        private const string KodPrefix = "UN";
        private readonly CastlePlus2DbContext _db;

        public UmowaNajmuKodGenerator(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<string> GenerateUmowaNajmuKodAsync(DateOnly dataZawarcia, CancellationToken ct)
        {
            var year = dataZawarcia.Year;
            var prefix = $"{KodPrefix}/{year}/";

            var existingCodes = await _db.Encje
                .AsNoTracking()
                .Where(encja => encja.TypEncji == TypEncjiUmowy && encja.KodEncji != null)
                .Where(encja => encja.KodEncji!.StartsWith(prefix))
                .Select(encja => encja.KodEncji!)
                .ToListAsync(ct);

            var maxNumber = 0;
            foreach (var code in existingCodes)
            {
                if (!code.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var suffix = code.Substring(prefix.Length);
                if (int.TryParse(suffix, NumberStyles.None, CultureInfo.InvariantCulture, out var parsed))
                {
                    if (parsed > maxNumber)
                    {
                        maxNumber = parsed;
                    }
                }
            }

            var nextNumber = maxNumber + 1;
            return $"{prefix}{nextNumber:0000}";
        }
    }
}