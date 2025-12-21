using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Adresy.Queries.GetAllAdresy
{
    public class GetAllAdresyQueryHandler : IRequestHandler<GetAllAdresyQuery, List<AdresDto>>
    {
        private readonly IAdresRepository _repo;
        private readonly IMapper _mapper;

        public GetAllAdresyQueryHandler(IAdresRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<AdresDto>> Handle(GetAllAdresyQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(a => _mapper.Map<AdresDto>(a)).ToList();
        }
    }
}
