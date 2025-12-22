using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Przylacza.Queries.GetAllPrzylacza
{
    public sealed class GetAllPrzylaczaQuery : IRequest<List<PrzylaczeDto>>
    {
    }
}
