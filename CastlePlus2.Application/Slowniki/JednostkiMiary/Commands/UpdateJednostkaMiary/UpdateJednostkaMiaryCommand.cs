using MediatR;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.UpdateJednostkaMiary
{
    public class UpdateJednostkaMiaryCommand : IRequest
    {
        public string KodJednostki { get; set; } = default!;
        public string Nazwa { get; set; } = default!;
    }
}
