using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Queries.GetWlasnoscById
{
    public class GetWlasnoscByIdQueryHandler : IRequestHandler<GetWlasnoscByIdQuery, WlasnoscDto?>
    {
        private readonly IWlasnoscRepository _repo;
        private readonly IMapper _mapper;

        public GetWlasnoscByIdQueryHandler(IWlasnoscRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<WlasnoscDto?> Handle(GetWlasnoscByIdQuery request, CancellationToken ct)
        {
            if (request.IdWlasnosci <= 0)
                return null;

            var entity = await _repo.GetByIdAsync(request.IdWlasnosci, ct);
            return entity is null ? null : _mapper.Map<WlasnoscDto>(entity);
        }
    }
}
