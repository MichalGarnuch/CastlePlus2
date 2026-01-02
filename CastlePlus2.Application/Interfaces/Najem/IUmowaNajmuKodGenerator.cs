using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastlePlus2.Application.Interfaces.Najem
{
    public interface IUmowaNajmuKodGenerator
    {
        Task<string> GenerateUmowaNajmuKodAsync(DateOnly dataZawarcia, CancellationToken ct);
    }
}
