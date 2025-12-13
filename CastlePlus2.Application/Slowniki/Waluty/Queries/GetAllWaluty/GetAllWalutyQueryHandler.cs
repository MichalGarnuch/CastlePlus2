using AutoMapper;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Contracts.DTOs.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.Waluty.Queries.GetAllWaluty
{
    public class GetAllWalutyQueryHandler : IRequestHandler<GetAllWalutyQuery, List<WalutaDto>>
    {
        private readonly IWalutaRepository _repo;
        private readonly IMapper _mapper;

        public GetAllWalutyQueryHandler(IWalutaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<WalutaDto>> Handle(GetAllWalutyQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(x => _mapper.Map<WalutaDto>(x)).ToList();
        }
    }
}
