using AutoMapper;
using CastlePlus2.Application.Interfaces.Konfiguracja;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUI.Queries.GetAllZasobyUI
{
    public class GetAllZasobyUIQueryHandler : IRequestHandler<GetAllZasobyUIQuery, List<ZasobUIDto>>
    {
        private readonly IZasobUIRepository _repo;
        private readonly IMapper _mapper;

        public GetAllZasobyUIQueryHandler(IZasobUIRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ZasobUIDto>> Handle(GetAllZasobyUIQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(request.Typ, request.Kategoria, request.CzyAktywny, ct);
            return list.Select(x => _mapper.Map<ZasobUIDto>(x)).ToList();
        }
    }
}