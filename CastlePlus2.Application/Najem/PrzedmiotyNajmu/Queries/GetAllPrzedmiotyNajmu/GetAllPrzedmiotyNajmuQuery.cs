using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Queries.GetAllPrzedmiotyNajmu
{
    public class GetAllPrzedmiotyNajmuQuery : IRequest<List<PrzedmiotNajmuDto>>
    {
    }
}
