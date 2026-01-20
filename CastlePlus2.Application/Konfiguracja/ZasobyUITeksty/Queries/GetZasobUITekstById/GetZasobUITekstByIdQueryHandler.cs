using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Konfiguracja;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Queries.GetZasobUITekstById
{
    public sealed class GetZasobUITekstByIdQueryHandler : IRequestHandler<GetZasobUITekstByIdQuery, ZasobUITekstDto?>
    {
        private readonly IZasobUITekstRepository _repo;
        private readonly IMapper _mapper;

        public GetZasobUITekstByIdQueryHandler(IZasobUITekstRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ZasobUITekstDto?> Handle(GetZasobUITekstByIdQuery request, CancellationToken cancellationToken)
        {
            var ent = await _repo.GetByIdAsync(request.IdZasobuTekstu, cancellationToken);
            return ent is null ? null : _mapper.Map<ZasobUITekstDto>(ent);
        }
    }
}
