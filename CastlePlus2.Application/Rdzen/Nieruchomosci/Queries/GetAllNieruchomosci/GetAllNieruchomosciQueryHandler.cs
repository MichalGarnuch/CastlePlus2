using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Nieruchomosci.Queries.GetAllNieruchomosci
{
    public sealed class GetAllNieruchomosciQueryHandler : IRequestHandler<GetAllNieruchomosciQuery, List<NieruchomoscDto>>
    {
        private readonly INieruchomoscRepository _repo;
        private readonly IMapper _mapper;

        public GetAllNieruchomosciQueryHandler(INieruchomoscRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<NieruchomoscDto>> Handle(GetAllNieruchomosciQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(_mapper.Map<NieruchomoscDto>).ToList();
        }
    }
}
