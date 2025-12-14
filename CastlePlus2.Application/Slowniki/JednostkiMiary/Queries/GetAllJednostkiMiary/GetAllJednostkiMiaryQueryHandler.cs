using AutoMapper;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Contracts.DTOs.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Queries.GetAllJednostkiMiary
{
    public class GetAllJednostkiMiaryQueryHandler : IRequestHandler<GetAllJednostkiMiaryQuery, List<JednostkaMiaryDto>>
    {
        private readonly IJednostkaMiaryRepository _repo;
        private readonly IMapper _mapper;

        public GetAllJednostkiMiaryQueryHandler(IJednostkaMiaryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<JednostkaMiaryDto>> Handle(GetAllJednostkiMiaryQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(x => _mapper.Map<JednostkaMiaryDto>(x)).ToList();
        }
    }
}
