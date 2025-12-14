using CastlePlus2.Contracts.DTOs.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.CreateJednostkaMiary
{
    public class CreateJednostkaMiaryCommand : IRequest<JednostkaMiaryDto>
    {
        public string KodJednostki { get; set; } = default!;
        public string Nazwa { get; set; } = default!;
    }
}
