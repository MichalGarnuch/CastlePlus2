using CastlePlus2.Contracts.DTOs.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Queries.GetAllJednostkiMiary
{
    public class GetAllJednostkiMiaryQuery : IRequest<List<JednostkaMiaryDto>>
    {
    }
}
