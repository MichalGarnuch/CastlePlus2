using MediatR;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.DeleteJednostkaMiary
{
    public sealed record DeleteJednostkaMiaryCommand(string KodJednostki) : IRequest<bool>;
}