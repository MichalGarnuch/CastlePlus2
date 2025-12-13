using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.Dokumenty.Queries.GetDokumentById
{
    public class GetDokumentByIdQuery : IRequest<DokumentDto?>
    {
        public long Id { get; }

        public GetDokumentByIdQuery(long id)
        {
            Id = id;
        }
    }
}
