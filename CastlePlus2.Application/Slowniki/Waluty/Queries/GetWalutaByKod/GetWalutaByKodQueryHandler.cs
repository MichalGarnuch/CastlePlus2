using AutoMapper;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Contracts.DTOs.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.Waluty.Queries.GetWalutaByKod
{
    public class GetWalutaByKodQueryHandler : IRequestHandler<GetWalutaByKodQuery, WalutaDto?>
    {
        private readonly IWalutaRepository _repo;
        private readonly IMapper _mapper;

        public GetWalutaByKodQueryHandler(IWalutaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<WalutaDto?> Handle(GetWalutaByKodQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByKodAsync(request.KodWaluty, ct);
            return entity == null ? null : _mapper.Map<WalutaDto>(entity);
        }
    }
}
