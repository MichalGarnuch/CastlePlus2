using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Konfiguracja;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Queries.GetTekstyByZasob
{
    public sealed class GetTekstyByZasobQueryHandler : IRequestHandler<GetTekstyByZasobQuery, List<ZasobUITekstDto>>
    {
        private readonly IZasobUITekstRepository _repo;
        private readonly IMapper _mapper;

        public GetTekstyByZasobQueryHandler(IZasobUITekstRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ZasobUITekstDto>> Handle(GetTekstyByZasobQuery request, CancellationToken cancellationToken)
        {
            var list = await _repo.GetByEncjaIdAsync(request.IdEncji, cancellationToken);
            return _mapper.Map<List<ZasobUITekstDto>>(list);
        }
    }
}
