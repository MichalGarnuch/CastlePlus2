using CastlePlus2.Contracts.DTOs.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Queries.GetJednostkaMiaryByKod
{
    public class GetJednostkaMiaryByKodQuery : IRequest<JednostkaMiaryDto?>
    {
        public string KodJednostki { get; set; } = default!;
    }
}
