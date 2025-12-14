using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Kontakty.Queries.GetKontaktById
{
    public class GetKontaktByIdQuery : IRequest<KontaktDto?>
    {
        public long IdKontaktu { get; }

        public GetKontaktByIdQuery(long idKontaktu)
        {
            IdKontaktu = idKontaktu;
        }
    }
}
