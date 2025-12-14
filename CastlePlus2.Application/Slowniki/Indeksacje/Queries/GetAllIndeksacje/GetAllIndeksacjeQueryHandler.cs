using AutoMapper;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Contracts.DTOs.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.Indeksacje.Queries.GetAllIndeksacje
{
    public class GetAllIndeksacjeQueryHandler : IRequestHandler<GetAllIndeksacjeQuery, List<IndeksacjaDto>>
    {
        private readonly IIndeksacjaRepository _repo;
        private readonly IMapper _mapper;

        public GetAllIndeksacjeQueryHandler(IIndeksacjaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<IndeksacjaDto>> Handle(GetAllIndeksacjeQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(x => _mapper.Map<IndeksacjaDto>(x)).ToList();
        }
    }
}
