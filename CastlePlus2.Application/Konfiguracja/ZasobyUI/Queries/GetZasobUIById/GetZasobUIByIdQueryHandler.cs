using AutoMapper;
using CastlePlus2.Application.Interfaces.Konfiguracja;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUI.Queries.GetZasobUIById
{
    public class GetZasobUIByIdQueryHandler : IRequestHandler<GetZasobUIByIdQuery, ZasobUIDto?>
    {
        private readonly IZasobUIRepository _repo;
        private readonly IMapper _mapper;

        public GetZasobUIByIdQueryHandler(IZasobUIRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ZasobUIDto?> Handle(GetZasobUIByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.IdEncji, ct);
            return entity == null ? null : _mapper.Map<ZasobUIDto>(entity);
        }
    }
}