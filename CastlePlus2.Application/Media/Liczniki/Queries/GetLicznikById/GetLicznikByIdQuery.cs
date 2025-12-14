using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Liczniki.Queries.GetLicznikById
{
    public class GetLicznikByIdQuery : IRequest<LicznikDto?>
    {
        public long IdLicznika { get; }

        public GetLicznikByIdQuery(long idLicznika)
        {
            IdLicznika = idLicznika;
        }
    }
}
