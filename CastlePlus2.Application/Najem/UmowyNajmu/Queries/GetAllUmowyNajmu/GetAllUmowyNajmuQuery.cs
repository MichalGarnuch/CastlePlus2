using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Queries.GetAllUmowyNajmu
{
    public class GetAllUmowyNajmuQuery : IRequest<List<UmowaNajmuDto>>
    {
    }
}
