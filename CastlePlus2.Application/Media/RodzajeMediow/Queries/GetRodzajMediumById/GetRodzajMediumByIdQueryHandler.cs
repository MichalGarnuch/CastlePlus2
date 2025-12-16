using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.RodzajeMediow.Queries.GetRodzajMediumById
{
    public sealed class GetRodzajMediumByIdQueryHandler : IRequestHandler<GetRodzajMediumByIdQuery, RodzajMediumDto?>
    {
        private readonly IRodzajMediumRepository _repo;
        private readonly IMapper _mapper;

        public GetRodzajMediumByIdQueryHandler(IRodzajMediumRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RodzajMediumDto?> Handle(GetRodzajMediumByIdQuery request, CancellationToken ct)
        {
            var kod = (request.KodRodzaju ?? string.Empty).Trim();
            if (kod.Length == 0 || kod.Length > 20) return null;

            var entity = await _repo.GetByIdAsync(kod, ct);
            return entity is null ? null : _mapper.Map<RodzajMediumDto>(entity);
        }
    }
}
