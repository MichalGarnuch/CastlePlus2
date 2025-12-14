using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Queries.GetAllPrzedmiotyNajmu
{
    public class GetAllPrzedmiotyNajmuQueryHandler : IRequestHandler<GetAllPrzedmiotyNajmuQuery, List<PrzedmiotNajmuDto>>
    {
        private readonly IPrzedmiotNajmuRepository _repo;
        private readonly IMapper _mapper;

        public GetAllPrzedmiotyNajmuQueryHandler(IPrzedmiotNajmuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PrzedmiotNajmuDto>> Handle(GetAllPrzedmiotyNajmuQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(x => _mapper.Map<PrzedmiotNajmuDto>(x)).ToList();
        }
    }
}
