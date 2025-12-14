using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Queries.GetAllUmowyNajmu
{
    public class GetAllUmowyNajmuQueryHandler : IRequestHandler<GetAllUmowyNajmuQuery, List<UmowaNajmuDto>>
    {
        private readonly IUmowaNajmuRepository _repo;
        private readonly IMapper _mapper;

        public GetAllUmowyNajmuQueryHandler(IUmowaNajmuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<UmowaNajmuDto>> Handle(GetAllUmowyNajmuQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(x => _mapper.Map<UmowaNajmuDto>(x)).ToList();
        }
    }
}
