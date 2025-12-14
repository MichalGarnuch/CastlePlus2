using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.SkladnikiCzynszu.Queries.GetAllSkladnikiCzynszu
{
    public class GetAllSkladnikiCzynszuQuery : IRequest<List<SkladnikCzynszuDto>>
    {
    }
}
