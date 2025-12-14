using AutoMapper;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Contracts.DTOs.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.Indeksacje.Queries.GetIndeksacjaByKod
{
    public class GetIndeksacjaByKodQueryHandler : IRequestHandler<GetIndeksacjaByKodQuery, IndeksacjaDto?>
    {
        private readonly IIndeksacjaRepository _repo;
        private readonly IMapper _mapper;

        public GetIndeksacjaByKodQueryHandler(IIndeksacjaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IndeksacjaDto?> Handle(GetIndeksacjaByKodQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByKodAsync(request.KodIndeksacji, ct);
            return entity == null ? null : _mapper.Map<IndeksacjaDto>(entity);
        }
    }
}
