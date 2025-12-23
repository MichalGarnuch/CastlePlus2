using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Encje.Queries.GetAllEncje
{
    public record GetAllEncjeQuery() : IRequest<List<EncjaDto>>;
}