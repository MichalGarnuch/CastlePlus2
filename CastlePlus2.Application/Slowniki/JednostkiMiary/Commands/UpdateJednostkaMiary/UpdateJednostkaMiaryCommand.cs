using MediatR;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.UpdateJednostkaMiary
{
    public sealed class UpdateJednostkaMiaryCommand : IRequest<bool>
    {
        public string KodJednostki { get; set; } = default!;
        public string Nazwa { get; set; } = default!;
    }
}