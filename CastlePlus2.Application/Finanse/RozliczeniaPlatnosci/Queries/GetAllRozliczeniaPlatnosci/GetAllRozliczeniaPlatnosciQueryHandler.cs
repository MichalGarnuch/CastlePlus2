using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Queries.GetAllRozliczeniaPlatnosci
{
    public class GetAllRozliczeniaPlatnosciQueryHandler
        : IRequestHandler<GetAllRozliczeniaPlatnosciQuery, List<RozliczeniePlatnosciDto>>
    {
        private readonly IRozliczeniePlatnosciRepository _repo;
        private readonly IMapper _mapper;

        public GetAllRozliczeniaPlatnosciQueryHandler(IRozliczeniePlatnosciRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<RozliczeniePlatnosciDto>> Handle(GetAllRozliczeniaPlatnosciQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(x => _mapper.Map<RozliczeniePlatnosciDto>(x)).ToList();
        }
    }
}
