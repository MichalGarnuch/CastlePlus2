using AutoMapper;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Contracts.DTOs.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Queries.GetJednostkaMiaryByKod
{
    public class GetJednostkaMiaryByKodQueryHandler : IRequestHandler<GetJednostkaMiaryByKodQuery, JednostkaMiaryDto?>
    {
        private readonly IJednostkaMiaryRepository _repo;
        private readonly IMapper _mapper;

        public GetJednostkaMiaryByKodQueryHandler(IJednostkaMiaryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<JednostkaMiaryDto?> Handle(GetJednostkaMiaryByKodQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByKodAsync(request.KodJednostki, ct);
            return entity == null ? null : _mapper.Map<JednostkaMiaryDto>(entity);
        }
    }
}
