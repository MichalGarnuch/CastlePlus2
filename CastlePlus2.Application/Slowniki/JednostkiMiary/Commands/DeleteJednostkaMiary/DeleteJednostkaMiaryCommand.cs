using MediatR;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.DeleteJednostkaMiary
{
    public class DeleteJednostkaMiaryCommand : IRequest
    {
        public string KodJednostki { get; set; } = default!;
    }
}
